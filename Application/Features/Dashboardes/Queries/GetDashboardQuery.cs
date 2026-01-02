using Application.Dto.Dashboardes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.CatagoryTypes;
using Domain.Entities.Transicstions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dashboards.Queries;

public class GetAllDashBoardQuery : IRequest<Result<GetDashboardDto>>
{
}

internal class GetAllDashBoardQueryHandler : IRequestHandler<GetAllDashBoardQuery, Result<GetDashboardDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDashBoardQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetDashboardDto>> Handle(GetAllDashBoardQuery request, CancellationToken cancellationToken)
    {
        var transicstions = _unitOfWork
            .Repository<Transicstion>()
            .Entities
            .AsQueryable();

        var totalIncome = await transicstions
            .Where(x => x.Catgory.CatgoryType == CatgoryType.salary)
            .SumAsync(x => (int?)x.Amount) ?? 0;

        var totalExpense = await transicstions
            .Where(x => x.Catgory.CatgoryType == CatgoryType.expence)
            .SumAsync(x => (int?)x.Amount) ?? 0;

        var totalBalance = totalIncome - totalExpense;

        var dto = new GetDashboardDto
        {
            Salery = totalIncome,
            Expense = totalExpense,
            TotalAmount = totalBalance
        };

        return Result<GetDashboardDto>.Success(dto, "Dashboard summary");
    }
}
