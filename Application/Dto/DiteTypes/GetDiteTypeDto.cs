using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.DiteTypes;

namespace Application.Dto.diteTypes
{
    public class GetDiteTypeDto : BaseDto,IMapFrom<DiteType>
    {
        public string Name { get; set; }
    }
}