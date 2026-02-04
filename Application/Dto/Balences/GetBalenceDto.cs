using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Customers;
using Application.Features.Categoryes.Command;
using Domain.Common.Enums.BalanceTypes;
using Domain.Entities.Balances;

namespace Application.Dto.Balences;

public class GetBalenceDto: BaseDto, IMapFrom<Balance>
{
    public int? CustomerId { get; set; }
    public GetCustomerDto Customer { get; set; }
    public BalanceType BalanceType { get; set; }
    public decimal Amount { get; set; } 
}