

using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Gyms;

namespace Application.Dto.Gyms
{
    public class GetGymDto : BaseDto,IMapFrom<Gym>
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }
        public int? LocationId { get; set; }
    }
}
