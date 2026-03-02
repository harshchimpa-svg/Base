using Application.Dto.Users.GetUserDtos;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using AutoMapper;
using Domain.Common.Enums.Users;
using Domain.Entities.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Queries;

public class GetCurrentUserQuery : IRequest<PaginatedResult<GetUserDto>>
{
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string? Name { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

internal class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, PaginatedResult<GetUserDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GetCurrentUserQueryHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<PaginatedResult<GetUserDto>> Handle(GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var search = request;

        var queryable = _userManager.Users
            .Include(x => x.UserAddress)
            .Include(x => x.UserAddress)
            .Include(x => x.UserProfile)
            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .Where(u => u.UserType == UserType.WebUser &&
                        (string.IsNullOrEmpty(search.Name) || u.FirstName.ToLower().Contains(search.Name.ToLower()))
                        && (string.IsNullOrEmpty(search.MobileNumber) || u.PhoneNumber.Contains(search.MobileNumber))
                        && (string.IsNullOrEmpty(search.Email) || u.Email.ToLower().Contains(search.Email.ToLower())))
            .AsQueryable();

        var count = await queryable.CountAsync();

        var users = await queryable
            .OrderByDescending(x => x.CreatedDate)
            .Skip((search.PageNumber - 1) * search.PageSize)
            .Take(search.PageSize)
            .ToListAsync();

        var mapUser = _mapper.Map<List<GetUserDto>>(users);

        return PaginatedResult<GetUserDto>.Create(mapUser, count, search.PageNumber, search.PageSize, 200);
    }
}