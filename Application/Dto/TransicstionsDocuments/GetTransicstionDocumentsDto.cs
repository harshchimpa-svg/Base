using Application.Common.Mappings.Commons;
using Application.Dto.Catgoryes;
using Application.Dto.CommonDtos;
using Application.Dto.Transicstiones;
using Domain.Entities.Transicstions;
using Domain.Entities.TranstionDocuments;

namespace Application.Dto.Transicstions;

public class GetTransicstionDocumentsDto : BaseDto, IMapFrom<TranstionDocument>
{
    public int? CatgoryId { get; set; }
    public GetCatgoryDto Catgory { get; set; }
    public int? TransicstionId { get; set; }
    public GetTransicstionDto GetTransicstionDto { get; set; }
} 
