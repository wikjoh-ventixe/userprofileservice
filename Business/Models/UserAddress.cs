using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class UserAddress
{
    public string? StreetAddress { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}
