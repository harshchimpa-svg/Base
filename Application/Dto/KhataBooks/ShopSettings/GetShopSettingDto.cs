using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Services;
using Domain.Entities.ShopSettings;

namespace Application.Dto.ShopSettings;

public class GetShopSettingDto: BaseDto, IMapFrom<ShopSetting>
{
    public string ShopeName { get; set; }
    public string OnerName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public int GstNumber  { get; set; }
    public int? EmployeeId { get; set; }
    public string? UserId { get; set; }
}