

using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.GymDocuments;

namespace Application.Dto.GymDocuments;

public class GetGymDocumentDto : BaseDto, IMapFrom<GymDocument>
{
    public string ImageUrl { get; set; }
    public int? GymId { get; set; }
}
