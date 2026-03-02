using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.Services;

namespace Domain.Entities.Clientses;

public class Client:BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    [ForeignKey("Service")]
    public int? ServiceId { get; set; }
    public Service Service { get; set; }
    public decimal Quantity  { get; set; }
}