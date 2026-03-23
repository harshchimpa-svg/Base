using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Abouts;

namespace Application.Dto.Abouts;

public class GetAboutDto: BaseDto, IMapFrom<About>
{
    public string Name { get; set; }
    public string Profile { get; set; }
    public string SubTitel { get; set; }
}