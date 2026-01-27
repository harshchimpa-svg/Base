using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.DietTypes;

namespace Application.Dto.dietTypes
{
    public class GetDietTypeDto : BaseDto,IMapFrom<DietType>
    {
        public string Name { get; set; }
    }
}