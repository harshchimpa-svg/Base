using Application.Common.Mappings.Commons;
using Domain.Entities.UserAddresses;

namespace Application.Dto.Users;

public class GetUserAddressDto : IMapFrom<UserAddress>
{
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }   
    public string? Country { get; set; }
    public int? PinCode { get; set; }
}
