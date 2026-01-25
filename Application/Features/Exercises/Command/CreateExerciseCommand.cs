using Application.Common.Mappings.Commons;
using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietTypes;
using Domain.Entities.Exercises;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.Exercises.Command;

public class CreateExerciseCommand: IRequest<Result<string>>, ICreateMapFrom<Exercise>
{
    public int? DietTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}

internal class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        if (request.DietTypeId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<DietType>().GetByID(request.DietTypeId.Value);
            if (parentExists == null)
            {
                return Result<string>.BadRequest("Exercise id not exit");
            }
        }

        var gym = _mapper.Map<Exercise>(request);

        await _unitOfWork.Repository<Exercise>().AddAsync(gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Exercise Created Successfully");
        
    }
}