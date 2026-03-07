

using Domain.Common;

namespace Domain.Entities.GymCategorys;

public class GymCategories : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
