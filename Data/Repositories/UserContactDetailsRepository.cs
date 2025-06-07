using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UserContactDetailsRepository(UserProfileDbContext context) : BaseRepository<UserContactDetailsEntity>(context), IUserContactDetailsRepository
{
}
