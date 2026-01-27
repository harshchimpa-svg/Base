using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Exercises;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Exercises.Command;

public class UpdateExerciseCommand: IRequest<Result<Exercise>>
{
    public int Id { get; set; }

    public CreateExerciseCommand CreateCommand { get; set; } = new();

    public UpdateExerciseCommand(int id, CreateExerciseCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}

internal class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, Result<Exercise>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Result<Exercise>> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.DietTypeId.HasValue)
        {
            var location = await _unitOfWork.Repository<Exercise>().GetByID(request.CreateCommand.DietTypeId.Value);
            
            if (location == null)
            {
                return Result<Exercise>.BadRequest("DietTypeId is not exist");
            }
        }
        var gym = await _unitOfWork.Repository<Exercise>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
        {
            if (gym == null)
            {
                return Result<Exercise>.BadRequest("Exercise id not found");
            }

            _mapper.Map(request.CreateCommand, gym);

            await _unitOfWork.Repository<Exercise>().UpdateAsync(gym);
            await _unitOfWork.Save(cancellationToken);

            return Result<Exercise>.Success("Updated Exercise");
        }
    }
}