

using Domain.Common;

namespace Domain.Entities.GymCategories;

public class GymCategoryes : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
