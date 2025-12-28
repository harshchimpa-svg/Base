using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Transicstions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Transicstiones.Commands;

public class DeleteTransicstionCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteTransicstionCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteTransicstionCommandHandler : IRequestHandler<DeleteTransicstionCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransicstionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteTransicstionCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Transicstion>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Location not found.");
        }

        await _unitOfWork.Repository<Transicstion>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Location deleted successfully.");
    }
}