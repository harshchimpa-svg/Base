

using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.GymMemerships;

namespace Application.Dto.GymMemerships;

public class GetUserMembershipDto :BaseDto,IMapFrom<UserMembership>
{
    public string UserId { get; set; }
    public int MembershipId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
