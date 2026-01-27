
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Gyms;

namespace Domain.Entities.GymDocuments;

public class GymDocument : BaseAuditableEntity
{
      public string ImageUrl { get; set; }

    [ForeignKey("Gym")]
    public int? GymId { get; set; }
    public Gym Gym { get; set; }
}
