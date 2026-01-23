using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Dites;

namespace Application.Dto.Dites;

public class GetDietDto: BaseDto,IMapFrom<diet>
{
    public int? DiteTypeId { get; set; }
    public   string Name { get; set; }
    public DateTime Time { get; set; }
    public string Description { get; set; }
}