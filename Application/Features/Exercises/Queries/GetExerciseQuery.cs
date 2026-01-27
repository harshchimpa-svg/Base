using Application.Dto.Exercises;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Diets;
using Domain.Entities.Exercises;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Exercises.Queries;

public class GetExerciseQuery: IRequest<PaginatedResult<GetExerciseDto>>
{
    public int? DietTypeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetExerciseQueryHandler : IRequestHandler<GetExerciseQuery,PaginatedResult<GetExerciseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetExerciseQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetExerciseDto>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Exercise>().Entities.Include(s => s.DietType)
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

        var map = _mapper.Map<List<GetExerciseDto>>(query);

        return PaginatedResult < GetExerciseDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}