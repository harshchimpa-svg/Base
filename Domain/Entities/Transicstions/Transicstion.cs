using Domain.Common;
using Domain.Common.Enums.TransicstionTypes;
using Domain.Entities.Catagoryes;
using Domain.Entities.PaymentHeates;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Transicstions;

public class Transicstion : BaseAuditableEntity
{
    public int Id { get; set; }

    [ForeignKey("Catgory")]
    public int? CatgoryId { get;set; }
    public Catgory Catgory { get; set; }
    public int Amount { get; set; }
    public string paticular {  get; set; }
    public string Comments { get; set; }
    public TransicstionType TransicstionType { get; set; }

    [ForeignKey("PaymentHead")]
    public int? PaymentHeadId { get; set; }
    public PaymentHead PaymentHead { get; set; }
}
