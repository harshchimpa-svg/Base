using Application.Dto.Dashboardes;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Balances;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dashboards.Queries;

public class GetAllDashBoardQuery : IRequest<Result<GetDashboardDto>>
{
}

internal class GetAllDashBoardQueryHandler
    : IRequestHandler<GetAllDashBoardQuery, Result<GetDashboardDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDashBoardQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetDashboardDto>> Handle(GetAllDashBoardQuery request, CancellationToken cancellationToken)
    {
        var balances = _unitOfWork
            .Repository<Balance>()
            .Entities
            .AsNoTracking();

        var totalCredit = await balances
            .SumAsync(x => (decimal?)x.Credit, cancellationToken) ?? 0;

        var totalDebit = await balances
            .SumAsync(x => (decimal?)x.Debit, cancellationToken) ?? 0;

        var totalBalance = totalCredit - totalDebit;

        var dto = new GetDashboardDto
        {
            Credit = totalCredit,
            Debit = totalDebit,
            TotalAmount = totalBalance
        };

        return Result<GetDashboardDto>.Success(dto, "Dashboard summary");
    }
}