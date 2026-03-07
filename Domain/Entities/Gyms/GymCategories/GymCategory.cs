

using Domain.Common;

namespace Domain.Entities.GymCategories;

public class GymCategory : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
