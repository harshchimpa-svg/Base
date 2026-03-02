using Domain.Common;

namespace Domain.Entities.DietTypes;

public class DietType:BaseAuditableEntity
{
    public string? Name { get; set; }
}