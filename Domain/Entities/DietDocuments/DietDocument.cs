using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Diets;

namespace Domain.Entities.DietDocuments;

public class DietDocument:BaseAuditableEntity
{
    [ForeignKey("DietId")]
    public int? DietId { get; set; }
    public Diet Diet { get; set; }
    public string Document { get; set; }
}