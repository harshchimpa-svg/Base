
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Common.Enums.LocationTypes;

namespace Domain.Entities.Locations;

public class Location : BaseAuditableEntity
{
    public string Name { get; set; }
    public LocationType? LocationType { get; set; }
    public string? ShortName { get; set; }
    public string? Code { get; set; }
    public int? ParentId { get; set; }
    public Location Parent { get; set; }
}
