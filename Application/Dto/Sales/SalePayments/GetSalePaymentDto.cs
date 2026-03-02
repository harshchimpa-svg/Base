using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Common.Enums;
using Domain.Common.Enums.Status;
using Domain.Entities.SalePayments;

namespace Application.Dto.SalePayments;

public class GetSalePaymentDto: BaseDto, IMapFrom<SalePayment>
{
    public int? SaleId { get; set; }
    public MethodType MethodType { get; set; }  
    public decimal NetAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public StatusType StatusType  { get; set; }
}