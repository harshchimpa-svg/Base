using Application.Dto.Dashboardes;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Common.Enums.BalanceTypes;
using Domain.Entities.Balances;
using Domain.Entities.PaymentLoges;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dashboards.Queries;

public class GetAllDashBoardQuery : IRequest<Result<GetDashboardDto>>
{
}

internal class GetAllDashBoardQueryHandler : IRequestHandler<GetAllDashBoardQuery, Result<GetDashboardDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDashBoardQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetDashboardDto>> Handle(GetAllDashBoardQuery request, CancellationToken cancellationToken)
    {
        var payments = _unitOfWork
            .Repository<PaymentLoge>()
            .Entities
            .AsNoTracking(); 

        var totalCredit = await payments
            .Where(x => x.BalanceType == BalanceType.Credit)
            .SumAsync(x => (decimal?)x.Amount, cancellationToken) ?? 0;

        var totalDebit = await payments
            .Where(x => x.BalanceType == BalanceType.Debit)
            .SumAsync(x => (decimal?)x.Amount, cancellationToken) ?? 0;

        var dto = new GetDashboardDto
        {
            Credit = totalCredit,
            Debit = totalDebit,
            TotalAmount = totalCredit - totalDebit
        };

        return Result<GetDashboardDto>.Success(dto, "Dashboard summary");
    }

}