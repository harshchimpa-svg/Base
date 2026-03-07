using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.GymCategories;

namespace Application.Dto.GymCategories;

public class GetGymCategoryDto : BaseDto,IMapFrom<GymCategoryes>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
