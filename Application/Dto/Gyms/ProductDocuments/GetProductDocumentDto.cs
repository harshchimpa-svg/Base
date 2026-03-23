using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.ProductDocuments;

namespace Application.Dto.ProductDocuments;

public class GetProductDocumentDto : BaseDto, IMapFrom<ProductDocument>
{
    public string ImageUrl { get; set; }
    public int? GymProductId { get; set; }
}
