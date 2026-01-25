using Application.Dto.Exercises;
using Application.Dto.Gyms;
using Application.Features.Gyms.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Exercises;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.Exercises.Queries;

public class GetByIdExerciseQuery: IRequest<Result<GetExerciseDto>>
{
    public int Id { get; set; }

    public GetByIdExerciseQuery(int id)
    {
        Id = id;
    }
}

internal class GetByIdExerciseQueryHandler : IRequestHandler<GetByIdExerciseQuery, Result<GetExerciseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdExerciseQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetExerciseDto>> Handle(GetByIdExerciseQuery request, CancellationToken cancellationToken)
    {
        var gym = await _unitOfWork.Repository<Exercise>().GetByID(request.Id);

        if (gym == null)
        {
            return Result<GetExerciseDto>.BadRequest("Exercise Not Found");
        }

        var mapData = _mapper.Map<GetExerciseDto>(gym);

        return Result<GetExerciseDto>.Success(mapData, "Exercise");
    }
}