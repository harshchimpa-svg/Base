using Domain.Common;
using Domain.Common.Enums.Employees;
using Domain.Commons.Enums.Users;
using Domain.Entities.ApplicationUsers;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Enums.Users.UserRoleType;

namespace Domain.Entities.UserProfiles;

public class UserProfile : BaseAuditableEntity
{
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public UserRoleType UserRoleType  { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string message { get; set; }
}
