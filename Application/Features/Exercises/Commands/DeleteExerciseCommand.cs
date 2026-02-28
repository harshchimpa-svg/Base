using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Exercises;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Exercises.Command;

public class DeleteExerciseCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteExerciseCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExerciseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _unitOfWork.Repository<Exercise>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!exercise)
        {
            return Result<bool>.BadRequest("Gym not Found");
        }

        await _unitOfWork.Repository<Exercise>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Exercise deleted succesfully");
    }
}