using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.ExerciseDocuments;

namespace Application.Dto.ExerciseDocuments;

public class GetExerciseDocumentDto: BaseDto,IMapFrom<ExerciseDocument>
{
    public int? ExerciseId { get; set; }
    public string Document { get; set; }
}