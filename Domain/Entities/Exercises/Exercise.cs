using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.DietTypes;

namespace Domain.Entities.Exercises;

public class Exercise:BaseAuditableEntity
{
    [ForeignKey("DietTypeId")]
    public int? DietTypeId { get; set; }
    public DietType? DietType { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}