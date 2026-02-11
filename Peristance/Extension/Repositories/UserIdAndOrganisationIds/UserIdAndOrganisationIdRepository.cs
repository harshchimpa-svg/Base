using Application.Dto.GetUserIdAndOrganizationIds;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Persistence.Extension.Repositories.UserIdAndOrganizationIds;

public class UserIdAndOrganizationIdRepository : IUserIdAndOrganizationIdRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserIdAndOrganizationIdRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetUserIdAndOrganizationIdDto> Get()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            throw new InvalidOperationException("HTTP context is not available.");
        }

        var result = new GetUserIdAndOrganizationIdDto
        {
            //UserId = "86eebc00-1ab5-487a-abf9-f5c87cf5d549",
            OrganizationId = 1
        };

        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            result.UserId = userId;
        }

        if (httpContext.Request.Headers.TryGetValue("OrganizationId", out var organizationId))
        {
            result.OrganizationId = Convert.ToInt32(organizationId);
        }
        
        return result;
    }
}
