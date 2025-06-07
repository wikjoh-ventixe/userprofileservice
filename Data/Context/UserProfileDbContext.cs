using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : DbContext(options)
{
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<UserContactDetailsEntity> UserContactDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfileEntity>()
            .HasOne(up => up.ContactDetails)
            .WithOne(cd => cd.UserProfile);

        base.OnModelCreating(modelBuilder);
    }
}
