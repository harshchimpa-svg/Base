using Application.Dto.Employees;
using Application.Dto.Users.GetUserDtos;
using Application.Features.Tranners.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Employees;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Employees.Queries;

public class GetAllEmployeeQueries: IRequest<PaginatedResult<GetUserDto>>

{
public string? Name { get; set; }
public string? MobileNumber { get; set; }
public string? Email { get; set; }
public int PageNumber { get; set; } = 1;
public int PageSize { get; set; } = 10;
}
internal class GetAllTrannerQueryHandler : IRequestHandler<GetAllEmployeeQueries, PaginatedResult<GetUserDto>>

{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTrannerQueryHandler(IMapper mapper, IUnitOfWork unitOfWork,UserManager<User> userManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetUserDto>> Handle(GetAllEmployeeQueries request, CancellationToken cancellationToken)

    {
        var search = request;

        var queryable = _userManager.Users
            .Include(x => x.UserAddress)
            .Include(x => x.UserAddress)
            .Include(x => x.UserProfile)
            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .Where(u => 
                (string.IsNullOrEmpty(search.Name) || u.FirstName.ToLower().Contains(search.Name.ToLower()))
                && (string.IsNullOrEmpty(search.MobileNumber) || u.PhoneNumber.Contains(search.MobileNumber))
                && (string.IsNullOrEmpty(search.Email) || u.Email.ToLower().Contains(search.Email.ToLower())))
            .AsQueryable();

        var count = await queryable.CountAsync();

        var users = await queryable
            .OrderByDescending(x => x.CreatedDate)
            .Skip((int)((search.PageNumber - 1) * search.PageSize))
            .Take((int)search.PageSize)
            .ToListAsync();

        var mapUser = _mapper.Map<List<GetUserDto>>(users);

        return PaginatedResult<GetUserDto>.Create(mapUser, count, search.PageNumber, search.PageSize, 200);
    }
}