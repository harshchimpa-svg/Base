

using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;

namespace Domain.Entities.GymCarts;

public class GymCart : BaseAuditableEntity
{
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
}
