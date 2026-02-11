using Application.Dto.Balences;
using Application.Dto.Customers;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Customers.Queries;

public class GetAllCustomerQueries : IRequest<PaginatedResult<GetCustomerDto>>
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal? Balance { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

internal class GetAllCustomersQueriesHandler : IRequestHandler<GetAllCustomerQueries, PaginatedResult<GetCustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCustomersQueriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<GetCustomerDto>> Handle(GetAllCustomerQueries request,
        CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork
            .Repository<Customer>()
            .Entities
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

        if (request.PageNumber != 0 && request.PageSize != 0)
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