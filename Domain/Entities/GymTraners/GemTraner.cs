/*using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;

namespace Domain.Entities.GymTraners;

public class GemTraner:BaseAuditableEntity
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
    [ForeignKey("Gym")]
    public int GymId { get; set; }
    public Gym Gym { get; set; }
}*/