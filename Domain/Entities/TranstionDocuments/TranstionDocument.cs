using Domain.Common;
using Domain.Entities.Catagoryes;
using Domain.Entities.Transicstions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TranstionDocuments;

public class TranstionDocument : BaseAuditableEntity
{
    [ForeignKey("Transicstion")]
    public int TransicstionId { get; set; }
    public Transicstion Transicstion { get; set; }

    [ForeignKey("Catgory")]
    public int? CatgoryId { get; set; }
    public Catgory Catgory { get; set; }
}
