using Domain.Common;

namespace Domain.Entities.DiteTypes;

public class DiteType:BaseAuditableEntity
{
    public string? Name { get; set; }
}