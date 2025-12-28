using Domain.Common;
using Domain.Common.Enums.CatagoryTypes;

namespace Domain.Entities.Catagoryes;

public class Catgory : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public string PhotoUrl { get; set; }
    public int? ParentId { get; set; }
    public Catgory Parent { get; set; }
    public CatgoryType CatgoryType { get; set; }            
}
