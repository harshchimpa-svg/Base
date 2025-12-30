using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Documents;

namespace Application.Dto.Documents;

public class GetDocumentDto : BaseDto, IMapFrom<Document>
{
    public string ImageUrl { get; set; }
}
