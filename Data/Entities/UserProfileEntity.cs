using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UserProfileEntity
{
    [Key]
    public string UserId { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";


    public UserContactDetailsEntity ContactDetails { get; set; } = null!;
    public UserAddressEntity Address { get; set; } = null!;
}
