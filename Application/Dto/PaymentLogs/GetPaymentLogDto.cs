using Application.Common.Mappings.Commons;
using Application.Dto.Balences;
using Application.Dto.CommonDtos;
using Application.Dto.Customers;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.PaymentLogs;
using Domain.Entities.Sales;

namespace Application.Dto.PaymentLoges;

public class GetPaymentLogDto: BaseDto, IMapFrom<PaymentLog>
{
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public int CustomerId { get; set; }
    public GetCustomerDto Customer { get; set; }
    public GetTransactionDto Transaction { get; set; }
}