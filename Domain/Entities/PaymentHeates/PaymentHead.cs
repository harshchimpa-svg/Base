using Domain.Common;
using Domain.Common.Enums.PaymentHeades;

namespace Domain.Entities.PaymentHeates;

public class PaymentHead : BaseAuditableEntity
{
    public string Name { get; set; }
    public PaymentHeadType PaymentHeadType { get; set; }
}
