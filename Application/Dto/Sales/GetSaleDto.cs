using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Sales;
using Domain.Entities.Services;

namespace Application.Dto.Sales;

public class GetSaleDto: BaseDto, IMapFrom<Sale>
{
    public string? UserId { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCanceld { get; set; }
    public string? InvoiceNo { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }
    public int? Tax  { get; set; }
}