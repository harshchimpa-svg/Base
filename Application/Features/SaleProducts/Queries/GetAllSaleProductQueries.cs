using Application.Dto.SaleProducts;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.SaleProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SaleProducts.Queries;

public class GetAllSaleProductQueries: IRequest<PaginatedResult<GetSaleProductDto>>
{
    public int? SaleId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllSaleProductQueriesHandler : IRequestHandler<GetAllSaleProductQueries,PaginatedResult<GetSaleProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSaleProductQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetSaleProductDto>> Handle(GetAllSaleProductQueries request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<SaleProduct>().Entities.Include(s => s.Sale)
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

        var map = _mapper.Map<List<GetSaleProductDto>>(query);

        return PaginatedResult < GetSaleProductDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}