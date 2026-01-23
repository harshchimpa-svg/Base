using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.DiteTypes;

namespace Domain.Entities.Dites;

public class diet:BaseAuditableEntity
{
 [ForeignKey("DiteTypeId")]
 public int? DiteTypeId { get; set; }
 public DiteType DiteType { get; set; }
 public   string Name { get; set; }
 public DateTime Time { get; set; }
 public string Description { get; set; }
}