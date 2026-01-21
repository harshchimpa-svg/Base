using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Catagoryes;
using Domain.Entities.Services;

namespace Application.Dto.Services;

public class GetServiceDto: BaseDto, IMapFrom<Service>
{
    public int? CatgoryId { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public int? VendorId { get; set; }
    public Decimal Mesurment { get; set; }
    public Decimal Price { get; set; }
}