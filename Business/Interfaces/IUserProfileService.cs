using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IUserProfileService
{
    Task<UserProfileResult<UserProfile?>> CreateUserProfileAsync(CreateUserProfileRequestDto request);
    Task<UserProfileResult<IEnumerable<UserProfile>>> GetAllUserProfilesAsync();
    Task<UserProfileResult<UserProfile?>> GetUserProfileByIdAsync(string id);
}
