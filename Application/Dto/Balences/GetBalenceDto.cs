using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Balances;

namespace Application.Dto.Balences;

public class GetBalenceDto: BaseDto, IMapFrom<Balance>
{
    public string? UserId { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
}