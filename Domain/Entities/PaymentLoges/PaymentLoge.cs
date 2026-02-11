using Domain.Common;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Transactions;
using Domain.Entities.Customers;

namespace Domain.Entities.PaymentLoges;

public class PaymentLoge : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public string? UserId { get; set; }
    public User User { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int TransactionId { get; set; }
    public Transaction Transaction { get; set; }
}