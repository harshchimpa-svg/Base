

using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Gyms.Command;

public class DeleteGymCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteGymCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
    {
        var gymExists = await _unitOfWork.Repository<Gym>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!gymExists)
        {
            return Result<bool>.BadRequest("Gym not Found");
        }

        await _unitOfWork.Repository<Gym>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Gym deleted succesfully");
    }
}
