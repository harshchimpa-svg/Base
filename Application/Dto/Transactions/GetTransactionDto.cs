using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Customers;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Transactions;

namespace Application.Dto.Balences;

public class GetTransactionDto: BaseDto, IMapFrom<Transaction>
{
    public int? CustomerId { get; set; }
    public GetCustomerDto Customer { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; } 
}