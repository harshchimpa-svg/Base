using System.Security.Claims;
using Application.Dto.PaymentLoges;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using Domain.Entities.PaymentLoges;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.PaymentLoges.Queries;

public class GetAllPaymentLogeQuery : IRequest<PaginatedResult<GetPaymentLogeDto>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public string? CustomerId { get; set; } 

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

internal class GetAllPaymentLogeQueryHandler
    : IRequestHandler<GetAllPaymentLogeQuery, PaginatedResult<GetPaymentLogeDto>>
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentLogeQueryHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetPaymentLogeDto>> Handle(GetAllPaymentLogeQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor
            .HttpContext?
            .User?
            .FindFirstValue(ClaimTypes.NameIdentifier);
        
        var queryable = _unitOfWork
            .Repository<PaymentLoge>()
            .Entities
            .Where(x => x.UserId == userId) 
            .Include(x => x.User)
            .Include(x => x.Customer)
            .Include(x => x.Transaction)
            .AsQueryable();

        if (request.FromDate.HasValue)
        {
            queryable = queryable.Where(x => x.CreatedDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            queryable = queryable.Where(x => x.CreatedDate <= request.ToDate.Value);
        }
        int count = await queryable.CountAsync(cancellationToken);

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .OrderByDescending(x => x.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var query = await queryable.AsNoTracking().ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetPaymentLogeDto>>(query);

        return PaginatedResult<GetPaymentLogeDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
