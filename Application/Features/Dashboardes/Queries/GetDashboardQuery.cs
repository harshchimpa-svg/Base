using Application.Dto.Dashboardes;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Transactions;
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
        var payments = await _unitOfWork
            .Repository<PaymentLoge>()
            .Entities
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var totalCredit = payments
            .Where(x => x.TransactionType == TransactionType.Credit)
            .Sum(x => x.Amount);

        var totalDebit = payments
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
