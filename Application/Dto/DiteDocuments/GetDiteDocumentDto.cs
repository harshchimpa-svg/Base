using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.DiteDocuments;
using Domain.Entities.Dites;

namespace Application.Dto.DiteDocuments;

public class GetDiteDocumentDto: BaseDto,IMapFrom<DiteDocument>
{
    public int? DiteId { get; set; }
    public string Document { get; set; }
}