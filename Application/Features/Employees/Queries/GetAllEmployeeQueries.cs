using Application.Dto.Employees;
using Application.Dto.Users.GetUserDtos;
using AutoMapper;
using Domain.Common.Enums.Users;
using Domain.Entities.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Employees.Queries;

public class GetAllEmployeeQueries: IRequest<PaginatedResult<GetUserDto>>
{
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string? Name { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

internal class GetAllEmployeeQueriesHandler : IRequestHandler<GetAllEmployeeQueries, PaginatedResult<GetUserDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllEmployeeQueriesHandler(
        IMapper mapper,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PaginatedResult<GetUserDto>> Handle(GetAllEmployeeQueries request, CancellationToken cancellationToken)
    {
        var currentUserId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserId))
            return PaginatedResult<GetUserDto>.Create(new List<GetUserDto>(), 0, request.PageNumber, request.PageSize, 401);

        var queryable = _userManager.Users
            .Include(x => x.UserAddress)
            .Include(x => x.UserProfile)
            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .Where(u => u.UserType == UserType.Employee
                        && u.CreatedBy == currentUserId 
                        && !u.IsDeleted
                        && (string.IsNullOrEmpty(request.Name) || u.FirstName.ToLower().Contains(request.Name.ToLower()))
                        && (string.IsNullOrEmpty(request.MobileNumber) || u.PhoneNumber.Contains(request.MobileNumber))
                        && (string.IsNullOrEmpty(request.Email) || u.Email.ToLower().Contains(request.Email.ToLower())))
            .AsQueryable();

        var count = await queryable.CountAsync(cancellationToken);

        var users = await queryable
            .OrderByDescending(x => x.CreatedDate)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var mapUser = _mapper.Map<List<GetUserDto>>(users);

        return PaginatedResult<GetUserDto>.Create(mapUser, count, request.PageNumber, request.PageSize, 200);
    }
}
