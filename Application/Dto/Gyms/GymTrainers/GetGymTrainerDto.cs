using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.GymTrainers;
using Domain.Entities.Vendors;

namespace Application.Dto.GymTrainers;

public class GetGymTrainerDto: BaseDto, IMapFrom<GymTrainer>
{
    public string UserId { get; set; }
    public int GymId { get; set; }
}