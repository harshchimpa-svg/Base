
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Locations;

namespace Domain.Entities.Gyms;

public class Gym : BaseAuditableEntity
{
    public string Name { get; set; }
    public int Price { get; set; }

    public string Description { get; set; }

    [ForeignKey("Location")]
    public int? LocationId { get; set; }
    public Location Location { get; set; }
        
}
