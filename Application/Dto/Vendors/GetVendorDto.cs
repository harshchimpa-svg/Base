using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Vendors;

namespace Application.Dto.Vendors;

public class GetVendorDto: BaseDto, IMapFrom<Vendor>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Profile { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
}