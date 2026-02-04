

using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.GymProducts;

namespace Domain.Entities.GymCartItem;

public class CartItem : BaseAuditableEntity
{
    public int Quantity { get; set; }

    [ForeignKey("Product")]
    public int? GymProductId { get; set; }
    public GymProduct GymProduct { get; set; }
}
