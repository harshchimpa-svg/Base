using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Common.Enums.PaymentHeades;
using Domain.Entities.PaymentHeates;

namespace Application.Dto.PaymentHeades;

public class GetPaymentHeadDto : BaseDto, IMapFrom<PaymentHead>
{
    public string Name { get; set; }
    public PaymentHeadType PaymentHeadType { get; set; }
}
