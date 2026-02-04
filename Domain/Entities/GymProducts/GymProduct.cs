

using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.GymCategorys;

namespace Domain.Entities.GymProducts;

public class GymProduct : BaseAuditableEntity
{
    public int Tax {  get; set; }
    public decimal Price { get; set; }

    [ForeignKey("Category")]
    public int? GymCategoryId { get; set; }
    public GymCategory GymCategory { get; set; }
}
