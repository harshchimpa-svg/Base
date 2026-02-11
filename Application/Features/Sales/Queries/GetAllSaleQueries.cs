using Application.Dto.Sales;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Sales.Queries;

public class GetAllSaleQueries: IRequest<PaginatedResult<GetSaleDto>>
{
    public string? UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllSaleQueriesHandler : IRequestHandler<GetAllSaleQueries,PaginatedResult<GetSaleDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSaleQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetSaleDto>> Handle(GetAllSaleQueries request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Sale>().Entities.Include(s => s.User).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.UserId))
        {
            queryable = queryable.Where(x => x.UserId == request.UserId);
        }

        int count = await queryable.CountAsync(cancellationToken);

        if (request.PageNumber > 0 && request.PageSize > 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var query = await queryable.ToListAsync(cancellationToken);
        var map = _mapper.Map<List<GetSaleDto>>(query);

        return PaginatedResult<GetSaleDto>.Create(map, count, request.PageNumber, request.PageSize);
    }

}