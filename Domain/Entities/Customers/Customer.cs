using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Transactions;

namespace Domain.Entities.Customers;

public class Customer: BaseAuditableEntity
{
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User? User { get; set; }
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Notes { get; set; }
    public decimal? Balance { get; set; }
    public string? Profile { get; set; }
}