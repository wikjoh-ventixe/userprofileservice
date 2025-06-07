using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Dtos;

public class CreateUserProfileRequestDto
{
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? StreetAddress { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}
