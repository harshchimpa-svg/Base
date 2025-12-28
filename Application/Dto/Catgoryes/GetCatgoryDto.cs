using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Common.Enums.CatagoryTypes;
using Domain.Entities.Catagoryes;

namespace Application.Dto.Catgoryes;

public class GetCatgoryDto : BaseDto, IMapFrom<Catgory>
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public string PhotoUrl { get; set; }
    public int? ParentId { get; set; }
    public CatgoryType CatgoryType { get; set; }
}
