using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Templates;

public class TemplateDocument
{
    [ForeignKey("Template")]
    public int TemplateId { get; set; }
    public Template Template { get; set; }
}
