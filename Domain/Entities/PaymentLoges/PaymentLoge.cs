using Domain.Common;
using Domain.Common.Enums.BalanceTypes;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Balances;
using Domain.Entities.Customers;

namespace Domain.Entities.PaymentLoges;

public class PaymentLoge : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public string? UserId { get; set; }
    public User User { get; set; }
    public decimal Amount { get; set; }
    public BalanceType BalanceType { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int BalanceId { get; set; }
    public Balance Balance { get; set; }
}