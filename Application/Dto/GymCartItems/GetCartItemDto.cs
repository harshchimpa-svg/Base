

using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.GymCartItem;

namespace Application.Dto.GymCartItems;

public class GetCartItemDto : BaseDto,IMapFrom<CartItem>
{
    public int Quantity { get; set; }
    public int? GymProductId { get; set; }
}
