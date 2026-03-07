using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.GymCategories;

namespace Domain.Entities.GymProducts;

public class GymProduct : BaseAuditableEntity
{
    public int Tax {  get; set; }
    public decimal Price { get; set; }

    [ForeignKey("GymCategoryes")]
    public int? GymCategoryId { get; set; }
    public GymCategoryes GymCategories { get; set; }
}
