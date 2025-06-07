using Data.Entities;

namespace Business.Models;

public class UserProfile
{

    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName { get; set; } = null!;


    public UserContactDetails ContactDetails { get; set; } = new();
    public UserAddress Address { get; set; } = new();
}
