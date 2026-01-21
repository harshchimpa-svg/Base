using Domain.Common;

namespace Domain.Entities.Customers;

public class Customer: BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public string Notes { get; set; }
}