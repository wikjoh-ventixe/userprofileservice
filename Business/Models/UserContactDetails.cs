using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class UserContactDetails
{
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
}
