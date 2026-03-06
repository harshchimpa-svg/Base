using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.GymProducts;
using Domain.Entities.Sales;

namespace Domain.Entities.SaleProducts;

public class SaleProduct:BaseAuditableEntity
{
    [ForeignKey("Sale")]
    public int? SaleId { get; set; }
    public Sale Sale { get; set; }
    [ForeignKey("Product")]
    public int? ProductId { get; set; }
    public GymProduct  Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; } 
}