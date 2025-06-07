using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserAddressEntity
{
    [Key]
    [ForeignKey(nameof(UserProfile))]
    public string UserId { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string? StreetAddress { get; set; }
    [Column(TypeName = "nvarchar(10)")]
    public string? PostalCode { get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public string? City { get; set; }

    public UserProfileEntity UserProfile { get; set; } = null!;
}
