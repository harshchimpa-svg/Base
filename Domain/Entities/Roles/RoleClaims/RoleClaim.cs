using Domain.Entities.ApplicationRoles;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Roles.RoleClaims;

public class RoleClaim : IdentityRoleClaim<string>
{

    [ForeignKey("Menu")]
    public Guid? MenuId { get; set; }
    public Role Role { get; set; }
}
