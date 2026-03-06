using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Categories;
using Domain.Entities.Vendors;

namespace Domain.Entities.Services;

public class Service:BaseAuditableEntity
{
    [ForeignKey("Category")]
    public int? CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    [ForeignKey("Vendor")]
    public int? VendorId { get; set; }
    public Vendor Vendor { get; set; }
    public Decimal Mesurment { get; set; }
    public Decimal Price { get; set; }
    
}