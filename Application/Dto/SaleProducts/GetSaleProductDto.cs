using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.SaleProducts;
using Domain.Entities.Sales;

namespace Application.Dto.SaleProducts;

public class GetSaleProductDto: BaseDto, IMapFrom<SaleProduct>
{
    public int? SaleId { get; set; }
    public int? ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal taxe { get; set; }
}