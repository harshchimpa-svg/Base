using Domain.Common;

namespace Domain.Entities.Carts;

public class Cart:BaseAuditableEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Rating { get; set; }
    
}