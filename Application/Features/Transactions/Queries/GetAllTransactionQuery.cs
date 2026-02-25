using Application.Dto.Balences;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Security.Claims;

namespace Application.Features.Balence.Queries;

public class GetAllTransactionQuery : IRequest<PaginatedResult<GetTransactionDto>>
{
    public int? CustomerId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

internal class GetAllBalenceQueryHandler : IRequestHandler<GetAllTransactionQuery, PaginatedResult<GetTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllBalenceQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PaginatedResult<GetTransactionDto>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            ?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
            return PaginatedResult<GetTransactionDto>.Create(new List<GetTransactionDto>(), 0, request.PageNumber, request.PageSize);

        var queryable = _unitOfWork.Repository<Transaction>()
            .Entities
            .Include(t => t.Customer)
            .Where(t => t.CreatedBy == userId)  
            .AsQueryable();

        if (request.CustomerId.HasValue)
            queryable = queryable.Where(x => x.CustomerId == request.CustomerId.Value);

        int count = await queryable.CountAsync(cancellationToken);

        if (request.PageNumber > 0 && request.PageSize > 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var query = await queryable.ToListAsync(cancellationToken);
        var map = _mapper.Map<List<GetTransactionDto>>(query);

        return PaginatedResult<GetTransactionDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
