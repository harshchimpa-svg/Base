using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Customers;

namespace Domain.Entities.Transactions;

public class Transaction: BaseAuditableEntity
{
    [ForeignKey("Customer")]
    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
}