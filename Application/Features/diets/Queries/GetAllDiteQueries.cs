using Application.Dto.Dites;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Dites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dites.Queries;

public class GetAllDiteQueries : IRequest<PaginatedResult<GetDietDto>>
{
    public int? DiteTypeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllDiteQueriesHandler : IRequestHandler<GetAllDiteQueries,PaginatedResult<GetDietDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDiteQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetDietDto>> Handle(GetAllDiteQueries request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<diet>().Entities.Include(s => s.DiteType)
            .AsQueryable();

        if (request.DiteTypeId.HasValue)
        {
            queryable = queryable.Where(x => x.DiteTypeId == request.DiteTypeId);
        }
        int count = await queryable.CountAsync();


        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetDietDto>>(query);

        return PaginatedResult < GetDietDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}

