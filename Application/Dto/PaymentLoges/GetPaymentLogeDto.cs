using Application.Common.Mappings.Commons;
using Application.Dto.Balences;
using Application.Dto.CommonDtos;
using Application.Dto.Customers;
using Domain.Common.Enums.BalanceTypes;
using Domain.Entities.PaymentLoges;
using Domain.Entities.Sales;

namespace Application.Dto.PaymentLoges;

public class GetPaymentLogeDto: BaseDto, IMapFrom<PaymentLoge>
{
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public decimal Amount { get; set; }
    public BalanceType BalanceType { get; set; }
    public int CustomerId { get; set; }
    public GetCustomerDto Customer { get; set; }
    public GetBalenceDto Balance { get; set; }
}