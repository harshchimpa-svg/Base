using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Exercises;

namespace Application.Dto.Exercises;

public class GetExerciseDto: BaseDto,IMapFrom<Exercise>
{
    public int? DietTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}