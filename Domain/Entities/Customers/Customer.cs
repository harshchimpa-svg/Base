using Domain.Common;
using Domain.Entities.Balances;

namespace Domain.Entities.Customers;

public class Customer: BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Notes { get; set; }
}