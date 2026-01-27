using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.DietTypes;

namespace Domain.Entities.Diets;

public class Diet:BaseAuditableEntity
{
 [ForeignKey("DietTypeId")]
 public int? DietTypeId { get; set; }
 public DietType DietType { get; set; }
 public   string Name { get; set; }
 public DateTime Time { get; set; }
 public string Description { get; set; }
}