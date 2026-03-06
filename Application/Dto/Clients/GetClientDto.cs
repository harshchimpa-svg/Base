using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Clients;

namespace Application.Dto.Clientses;

public class GetClientDto: BaseDto, IMapFrom<Client>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int? ServiceId { get; set; }
    public decimal Quantity  { get; set; }
}