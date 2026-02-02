using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;

namespace Domain.Entities.Sales;

public class Sale:BaseAuditableEntity
{
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public User User { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCanceld { get; set; }
    public string? InvoiceNo { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }
    public int? Tax  { get; set; }
}