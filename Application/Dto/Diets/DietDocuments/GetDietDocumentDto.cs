using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.DietDocuments;
using Domain.Entities.Diets;

namespace Application.Dto.DietDocuments;

public class GetDietDocumentDto: BaseDto,IMapFrom<DietDocument>
{
    public int? DietId { get; set; }
    public string Document { get; set; }
}