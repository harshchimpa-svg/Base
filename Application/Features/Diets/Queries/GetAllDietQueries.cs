using Application.Dto.Diets;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Diets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Diets.Queries;

public class GetAllDietQueries : IRequest<PaginatedResult<GetDietDto>>
{
    public int? DietTypeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllDietQueriesHandler : IRequestHandler<GetAllDietQueries,PaginatedResult<GetDietDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDietQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetDietDto>> Handle(GetAllDietQueries request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Diet>().Entities.Include(s => s.DietType)
            .AsQueryable();

        if (request.DietTypeId.HasValue)
        {
            queryable = queryable.Where(x => x.DietTypeId == request.DietTypeId);
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

