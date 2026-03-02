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

public class GetRecentActivityQuery : IRequest<Result<List<GetTransactionDto>>>
{
}

internal class GetRecentActivityQueryHandler : IRequestHandler<GetRecentActivityQuery, Result<List<GetTransactionDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetRecentActivityQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<List<GetTransactionDto>>> Handle(GetRecentActivityQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Result<List<GetTransactionDto>>.BadRequest("User not found");
        }

        var repository = _unitOfWork.Repository<Transaction>().Entities;

        var latestDates = repository
            .Where(t => t.CreatedBy == userId)
            .GroupBy(t => t.CustomerId)
            .Select(g => new
            {
                CustomerId = g.Key,
                CreatedDate = g.Max(x => x.CreatedDate)
            });

        var transactions = await repository
            .Where(t => t.CreatedBy == userId)
            .Join(latestDates,
                t => new { t.CustomerId, t.CreatedDate },
                l => new { l.CustomerId, l.CreatedDate },
                (t, l) => t)
            .OrderByDescending(t => t.CreatedDate)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<GetTransactionDto>>(transactions);

        return Result<List<GetTransactionDto>>.Success(result, "Latest transaction per customer");
    }
}