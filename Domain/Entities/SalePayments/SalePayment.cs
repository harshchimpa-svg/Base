using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Common.Enums;
using Domain.Common.Enums.Status;
using Domain.Entities.Sales;

namespace Domain.Entities.SalePayments;

public class SalePayment:BaseAuditableEntity
{
    [ForeignKey("SaleId")]
    public int? SaleId { get; set; }
    public Sale Sale { get; set; }
    public MethodType MethodType { get; set; }
    public decimal NetAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public StatusType StatusType  { get; set; }
}