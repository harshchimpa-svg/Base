
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.GymProducts;

namespace Domain.Entities.ProductDocuments;

public class ProductDocument : BaseAuditableEntity
{
    public string ImageUrl { get; set; }

    [ForeignKey("Product")]
    public int? GymProductId { get; set; }
    public GymProduct GymProduct { get; set; }
}
