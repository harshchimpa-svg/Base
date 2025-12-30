using Domain.Common;

namespace Domain.Entities.Documents;

public class Document : BaseAuditableEntity
{
    public string ImageUrl { get; set; }
}
