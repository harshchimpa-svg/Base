using Application.Common.Mappings.Commons;
using Application.Dto.Catgoryes;
using Application.Dto.CommonDtos;
using Application.Dto.PaymentHeades;
using Domain.Entities.Catagoryes;
using Domain.Entities.PaymentHeates;
using Domain.Entities.Transicstions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dto.Transicstiones;

public class GetTransicstionDto : BaseDto, IMapFrom<Transicstion> 
{
    public int? CatgoryId { get; set; }
    public GetCatgoryDto Catgory { get; set; }
    public int Amount { get; set; }
    public string paticular { get; set; }
    public string Comments { get; set; }
    public int? PaymentHeadId { get; set; }
    public GetPaymentHeadDto PaymentHead { get; set; }
}
