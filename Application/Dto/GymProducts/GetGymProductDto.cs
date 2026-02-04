

using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.GymProducts;
using Domain.Entities.ProductDocuments;

namespace Application.Dto.GymProducts;

public class GetGymProductDto : BaseDto,IMapFrom<GymProduct>
{
    public int Tax {  get; set; }
    public decimal Price { get; set; }
    public int? GymCategoryId { get; set; } 
}
