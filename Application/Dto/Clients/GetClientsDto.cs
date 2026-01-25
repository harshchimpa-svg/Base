using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Clientses;

namespace Application.Dto.Clientses;

public class GetClientsDto: BaseDto, IMapFrom<Clients>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int? ServiceId { get; set; }
    public decimal Quantity  { get; set; }
}