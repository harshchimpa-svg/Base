


using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.GymCategorys;

namespace Application.Dto.GymCategories;

public class GetGymCategoryDto : BaseDto,IMapFrom<GymCategory>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
