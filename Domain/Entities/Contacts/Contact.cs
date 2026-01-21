using Domain.Common;

namespace Domain.Entities.Contacts;

public class Contact: BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Massage { get; set; }
}