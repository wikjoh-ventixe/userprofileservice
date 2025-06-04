using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UserProfileRepository(UserProfileDbContext context) : BaseRepository<UserProfileEntity>(context), IUserProfileRepository
{
}
