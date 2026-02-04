using Application.Dto.SalePayments;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.SalePayments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SalePayments.Queries;

public class GetAllSalePaymentsQueries: IRequest<PaginatedResult<GetSalePaymentDto>>
{
    public int? SaleId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllSalePaymentsQueriesHandler : IRequestHandler<GetAllSalePaymentsQueries,PaginatedResult<GetSalePaymentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSalePaymentsQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetSalePaymentDto>> Handle(GetAllSalePaymentsQueries request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<SalePayment>().Entities.Include(s => s.Sale)
            .AsQueryable();

        if (request.SaleId.HasValue)
        {
            queryable = queryable.Where(x => x.SaleId == request.SaleId);
        }
        int count = await queryable.CountAsync();


        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetSalePaymentDto>>(query);

        return PaginatedResult < GetSalePaymentDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}