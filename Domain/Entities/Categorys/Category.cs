using Domain.Common;
using Domain.Common.Enums.CatagoryTypes;

namespace Domain.Entities.Catagoryes;

public class Category : BaseAuditableEntity
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
}
