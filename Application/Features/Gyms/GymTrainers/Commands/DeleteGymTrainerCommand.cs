using Application.Features.Customers.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Customers;
using Domain.Entities.GymTraners;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymTraners.Command;

public class DeleteGymTrainerCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteGymTrainerCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteGymTranerCommandHandler : IRequestHandler<DeleteGymTrainerCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymTranerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteGymTrainerCommand request, CancellationToken cancellationToken)
    {
        var gymTrainer = await _unitOfWork.Repository<GymTrainer>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!gymTrainer)
        {
            return Result<bool>.BadRequest("GymTrainer not found.");
        }

        await _unitOfWork.Repository<GymTrainer>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "GymTrainer deleted successfully.");
    }
}