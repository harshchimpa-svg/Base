using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;

namespace Domain.Entities.Balances;

public class Balance: BaseAuditableEntity
{
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User User { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
}