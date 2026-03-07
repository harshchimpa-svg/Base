

using Domain.Common;

namespace Domain.Entities.GymCategories;

public class GymCategories : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
