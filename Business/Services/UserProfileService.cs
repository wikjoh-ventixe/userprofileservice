using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace Business.Services;

public class UserProfileService(IUserProfileRepository userProfileRepository) : IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;


    // CREATE
    public async Task<UserProfileResult<UserProfile?>> CreateUserProfileAsync(CreateUserProfileRequestDto request)
    {
        if (request == null)
            return UserProfileResult<UserProfile?>.BadRequest("Request cannot be null.");

        try
        {
            var userProfileEntity = new UserProfileEntity
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ContactDetails = new UserContactDetailsEntity
                {
                    UserId = request.UserId,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                },
                Address = new UserAddressEntity
                {
                    UserId = request.UserId,
                    StreetAddress = request.StreetAddress,
                    PostalCode = request.PostalCode,
                    City = request.City,
                }
            };

            await _userProfileRepository.AddAsync(userProfileEntity);
            var result = await _userProfileRepository.SaveAsync();

            if (!result.Succeeded)
                return UserProfileResult<UserProfile?>.InternalServerError($"Failed creating user profile. {result.ErrorMessage}");

            var createdEntity = (await _userProfileRepository.GetOneAsync(x => x.UserId == request.UserId)).Data;
            if (createdEntity == null)
                return UserProfileResult<UserProfile?>.InternalServerError($"Failed retrieving user profile entity after creation.");

            var userProfile = new UserProfile
            {
                UserId = createdEntity.UserId,
                FirstName = createdEntity.FirstName,
                LastName = createdEntity.LastName,
                FullName = createdEntity.FullName,
                ContactDetails = new UserContactDetails
                {
                    Email = createdEntity.ContactDetails.Email,
                    PhoneNumber = createdEntity.ContactDetails.PhoneNumber,
                },
                Address = new UserAddress
                {
                    StreetAddress = createdEntity.Address.StreetAddress,
                    PostalCode = createdEntity.Address.PostalCode,
                    City = createdEntity.Address.City,
                }
            };

            return UserProfileResult<UserProfile?>.Created(userProfile);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return UserProfileResult<UserProfile?>.InternalServerError($"Exception occurred in {MethodBase.GetCurrentMethod()!.Name}.");
        }
    }


    // READ
    public async Task<UserProfileResult<IEnumerable<UserProfile>>> GetAllUserProfilesAsync()
    {
        var result = await _userProfileRepository.GetAllAsync(false, null, null, x => x.ContactDetails, x => x.Address );
        if (!result.Succeeded)
            return UserProfileResult<IEnumerable<UserProfile>>.InternalServerError($"Failed retrieving user profile entities. {result.ErrorMessage}");

        var entities = result.Data;
        var userProfiles = entities?.Select(x => new UserProfile
        {
            UserId = x.UserId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            FullName = x.LastName,
            ContactDetails = new UserContactDetails
            {
                Email = x.ContactDetails.Email,
                PhoneNumber = x.ContactDetails.PhoneNumber,
            },
            Address = new UserAddress
            {
                StreetAddress = x.Address.StreetAddress,
                City = x.Address.City,
                PostalCode = x.Address.PostalCode,
            }
        });

        return UserProfileResult<IEnumerable<UserProfile>>.Ok(userProfiles!);
    }

    public async Task<UserProfileResult<UserProfile?>> GetUserProfileByIdAsync(string id)
    {
        var result = await _userProfileRepository.GetOneAsync(x => x.UserId == id, x => x.ContactDetails, x => x.Address);
        if (!result.Succeeded || result.Data == null)
            return UserProfileResult<UserProfile?>.NotFound($"User profile with user id {id} not found.");

        var entity = result.Data;
        var userProfile = new UserProfile
        {
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            FullName = entity.LastName,
            ContactDetails = new UserContactDetails
            {
                Email = entity.ContactDetails.Email,
                PhoneNumber = entity.ContactDetails.PhoneNumber,
            },
            Address = new UserAddress
            {
                StreetAddress = entity.Address.StreetAddress,
                City = entity.Address.City,
                PostalCode = entity.Address.PostalCode,
            }
        };

        return UserProfileResult<UserProfile?>.Ok(userProfile);
    }
}
