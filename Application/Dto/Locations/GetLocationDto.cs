

using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Common.Enums.LocationTypes;
using Domain.Entities.Locations;

namespace Application.Dto.Locations;

public class GetLocationDto : BaseDto,IMapFrom<Location>
{
    
    public string Name { get; set; }
    
    public string Code {  get; set; }
    public string? ShortName { get; set; }
    public int? ParentId { get; set; }
    public LocationType? LocationType { get; set; }

    


}
