using Application.Dto.Balances;
using Application.Dto.Customers;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Customers.Queries;

public class GetAllCustomerQuery : IRequest<PaginatedResult<GetCustomerDto>>
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal? Balance { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomerQuery, PaginatedResult<GetCustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PaginatedResult<GetCustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor
            .HttpContext?
            .User?
            .FindFirstValue(ClaimTypes.NameIdentifier);

        var queryable = _unitOfWork
            .Repository<Customer>()
            .Entities
            .Where(x => x.UserId == userId) 
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            queryable = queryable.Where(x => x.Name.Contains(request.Name));
        }

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            queryable = queryable.Where(x => x.PhoneNumber.Contains(request.PhoneNumber));
        }

        if (request.Balance.HasValue)
        {
            queryable = queryable.Where(x => x.Balance >= request.Balance.Value);
        }

        if (request.StartDate.HasValue)
        {
            queryable = queryable.Where(x => x.CreatedDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            var endDate = request.EndDate.Value.Date.AddDays(1).AddTicks(-1);
            queryable = queryable.Where(x => x.CreatedDate <= endDate);
        }

        queryable = queryable.OrderByDescending(x => x.CreatedDate);

        int count = await queryable.CountAsync(cancellationToken);

        if (request.PageNumber > 0 && request.PageSize > 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var customers = await queryable.ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetCustomerDto>>(customers);

        return PaginatedResult<GetCustomerDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
