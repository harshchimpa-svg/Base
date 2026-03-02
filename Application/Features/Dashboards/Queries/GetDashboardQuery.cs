using System.Security.Claims;
using Application.Dto.Dashboardes;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dashboards.Queries;

public class GetAllDashBoardQuery : IRequest<Result<GetDashboardDto>>
{
}

internal class GetAllDashBoardQueryHandler : IRequestHandler<GetAllDashBoardQuery, Result<GetDashboardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllDashBoardQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<GetDashboardDto>> Handle(GetAllDashBoardQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            ?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
            return Result<GetDashboardDto>.BadRequest("User not authenticated");

        var transactions = await _unitOfWork
            .Repository<Transaction>()
            .Entities
            .AsNoTracking()
            .Where(x => x.CreatedBy == userId)   
            .ToListAsync(cancellationToken);

        var totalCredit = transactions
            .Where(x => x.TransactionType == TransactionType.Credit)
            .Sum(x => x.Amount);

        var totalDebit = transactions
            .Where(x => x.TransactionType == TransactionType.Debit)
            .Sum(x => x.Amount);

        var dto = new GetDashboardDto
        {
            Credit = totalCredit,
            Debit = totalDebit,
            TotalAmount = totalCredit - totalDebit
        };

        return Result<GetDashboardDto>.Success(dto, "Dashboard summary");
    }
}
