

using Domain.Common;

namespace Domain.Entities.GymCategorys;

public class GymCategory : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
