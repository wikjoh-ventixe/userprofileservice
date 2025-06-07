using Business.Dtos;
using Business.Interfaces;
using Grpc.Core;
using Protos;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Grpc.Services;

public class UserProfileService(IUserProfileService userProfileService) : GrpcUserProfile.GrpcUserProfileBase
{
    private readonly IUserProfileService _userProfileService = userProfileService;

    // CREATE
    public override async Task<CreateUserProfileResponse> CreateUserProfile(CreateUserProfileRequest request, ServerCallContext context)
    {
        Debug.WriteLine($"Creating user profile for user {request.UserId}.");

        var dto = new CreateUserProfileRequestDto
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            StreetAddress = request.StreetAddress,
            PostalCode = request.PostalCode,
            City = request.City,
        };

        var result = await _userProfileService.CreateUserProfileAsync(dto);

        return new CreateUserProfileResponse
        {
            Succeeded = result.Succeeded,
            StatusCode = result.StatusCode,
            ErrorMessage = result.ErrorMessage ?? string.Empty,
            UserId = request.UserId,
        };
    }


    // READ
    public override async Task<GetAllUserProfilesResponse> GetAllUserProfiles(GetAllUserProfilesRequest request, ServerCallContext context)
    {
        Debug.WriteLine($"Getting all user profiles");

        var result = await _userProfileService.GetAllUserProfilesAsync();

        var response = new GetAllUserProfilesResponse
        {
            Succeeded = result.Succeeded,
            StatusCode = result.StatusCode,
            ErrorMessage = result.ErrorMessage ?? string.Empty,
        };

        if (result.Data != null)
            foreach (var profile in result.Data)
                response.UserProfiles.Add(new Protos.UserProfile
                {
                    UserId = profile.UserId,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    FullName = profile.FullName,
                    ContactDetails = new Protos.ContactDetails
                    {
                        Email = profile.ContactDetails.Email,
                        PhoneNumber = profile.ContactDetails.PhoneNumber ?? string.Empty,
                    },
                    Address = new Protos.Address
                    {
                        StreetAddress = profile.Address.StreetAddress ?? string.Empty,
                        PostalCode = profile.Address.PostalCode ?? string.Empty,
                        City = profile.Address.City ?? string.Empty,
                    }
                });

        return response;
    }

    public override async Task<GetUserProfileResponse> GetUserProfile(GetUserProfileRequest request, ServerCallContext context)
    {
        Debug.WriteLine($"Getting user profile for user id {request.UserId}");

        var result = await _userProfileService.GetUserProfileByIdAsync(request.UserId);

        var response = new GetUserProfileResponse
        {
            Succeeded = result.Succeeded,
            StatusCode = result.StatusCode,
            ErrorMessage = result.ErrorMessage ?? string.Empty,
        };

        if (result.Data != null)
            response.UserProfile = new Protos.UserProfile
            {
                UserId = result.Data.UserId,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                FullName = result.Data.FullName,
                ContactDetails = new Protos.ContactDetails
                {
                    Email = result.Data.ContactDetails.Email,
                    PhoneNumber = result.Data.ContactDetails.PhoneNumber ?? string.Empty,
                },
                Address = new Protos.Address
                {
                    StreetAddress = result.Data.Address.StreetAddress ?? string.Empty,
                    PostalCode = result.Data.Address.PostalCode ?? string.Empty,
                    City = result.Data.Address.City ?? string.Empty,
                }
            };

        return response;
    }
}
