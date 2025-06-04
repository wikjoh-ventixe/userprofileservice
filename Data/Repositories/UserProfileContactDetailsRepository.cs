using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UserProfileContactDetailsRepository(UserProfileDbContext context) : BaseRepository<UserProfileContactDetailsEntity>(context), IUserProfileContactDetailsRepository
{
}
