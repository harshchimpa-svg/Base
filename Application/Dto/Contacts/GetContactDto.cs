using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Contacts;

namespace Application.Dto.Contacts;

public class GetContactDto : BaseDto, IMapFrom<Contact>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Massage { get; set; }
}