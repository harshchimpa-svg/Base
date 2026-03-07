using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.GymCategories;

namespace Domain.Entities.GymProducts;

public class GymProduct : BaseAuditableEntity
{
    public int Tax {  get; set; }
    public decimal Price { get; set; }

    [ForeignKey("GymCategory")]
    public int? GymCategoryId { get; set; }
    public GymCategory GymCategory{ get; set; }
}
