using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UserProfileContactDetailsEntity
{
    [Key]
    public string UserId { get; set; } = null!;

    [Column(TypeName = "varchar(20)")]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "varchar(256)")]
    public string? Email { get; set; }

    public UserProfileEntity? UserProfile { get; set; }
}
