using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Common.Enums.BalanceTypes;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Customers;

namespace Domain.Entities.Balances;

public class Balance: BaseAuditableEntity
{
    [ForeignKey("Customer")]
    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }
    public BalanceType BalanceType { get; set; }
    public decimal Amount { get; set; }
}