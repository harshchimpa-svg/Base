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

public class GetAllPaymentLogQuery : IRequest<PaginatedResult<GetPaymentLogDto>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public string? CustomerId { get; set; } 

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

internal class GetAllPaymentLogeQueryHandler : IRequestHandler<GetAllPaymentLogQuery, PaginatedResult<GetPaymentLogDto>>
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

    public async Task<PaginatedResult<GetPaymentLogDto>> Handle(GetAllPaymentLogQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor
            .HttpContext?
            .User?
            .FindFirstValue(ClaimTypes.NameIdentifier);
        
        var queryable = _unitOfWork
            .Repository<PaymentLog>()
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

        var map = _mapper.Map<List<GetPaymentLogDto>>(query);

        return PaginatedResult<GetPaymentLogDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
