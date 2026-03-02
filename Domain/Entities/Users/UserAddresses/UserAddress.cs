using Domain.Common;
using Domain.Entities.ApplicationUsers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAddresses;

public class UserAddress : BaseAuditableEntity
{
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }   
    public string? Country { get; set; }
    public int? PinCode { get; set; }
}
