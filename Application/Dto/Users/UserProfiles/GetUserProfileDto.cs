using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Common.Enums.Users.UserRoleType;
using Domain.Entities.UserProfiles;

namespace Application.Dto.Users;

public class GetUserProfileDto : IMapFrom<UserProfile>
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public UserRoleType UserRoleType  { get; set; }
    public decimal age { get; set; }
    public string message { get; set; }
}
