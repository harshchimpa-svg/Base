using Domain.Common;

namespace Domain.Entities.Abouts;

public class About:BaseAuditableEntity
{
    public string Name { get; set; }
    public string Profile { get; set; }
    public string SubTitel { get; set; }
}