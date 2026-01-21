using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Common.Enums.CatagoryTypes;
using Domain.Entities.Catagoryes;

namespace Application.Dto.Catgoryes;

public class GetCategoryDto : BaseDto, IMapFrom<Category>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public int? ParentId { get; set; }
}
