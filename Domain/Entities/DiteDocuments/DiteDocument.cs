using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Dites;

namespace Domain.Entities.DiteDocuments;

public class DiteDocument:BaseAuditableEntity
{
    [ForeignKey("DiteId")]
    public int? DiteId { get; set; }
    public diet Diet { get; set; }
    public string Document { get; set; }
}