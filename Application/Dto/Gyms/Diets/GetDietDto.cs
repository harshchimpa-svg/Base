using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Diets;

namespace Application.Dto.Diets;

public class GetDietDto: BaseDto,IMapFrom<Diet>
{
    public int? DietTypeId { get; set; }
    public   string Name { get; set; }
    public DateTime Time { get; set; }
    public string Description { get; set; }
}