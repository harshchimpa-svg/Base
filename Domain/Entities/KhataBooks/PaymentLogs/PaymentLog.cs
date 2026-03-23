using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Transactions;
using Domain.Entities.Customers;

namespace Domain.Entities.PaymentLogs;

public class PaymentLog : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User User { get; set; }
    
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public int TransactionId { get; set; }
    public Transaction Transaction { get; set; }
}