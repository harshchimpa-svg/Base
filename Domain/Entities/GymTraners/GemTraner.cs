using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Gyms;

namespace Domain.Entities.GymTraners;

public class GemTraner:BaseAuditableEntity
{
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
    [ForeignKey("Gym")]
    public int GymId { get; set; }
    public Gym Gym { get; set; }
}