using Domain.Common;

namespace Domain.Entities.Vendors;

public class Vendor :BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Profile { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
}