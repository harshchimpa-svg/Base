

using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;

namespace Domain.Entities.GymMemerships;

public class UserMembership : BaseAuditableEntity
{
    public int MembershipId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
}
