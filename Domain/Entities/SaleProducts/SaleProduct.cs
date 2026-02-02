using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Sales;

namespace Domain.Entities.SaleProducts;

public class SaleProduct:BaseAuditableEntity
{
    [ForeignKey("SaleId")]
    public int? SaleId { get; set; }
    public Sale Sale { get; set; }
    /*[ForeignKey("ProductId")]
    public int? ProductId { get; set; }
    public Product  Product { get; set; }*/
    
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal taxe { get; set; } 
}