

using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymDocuments.Command;

public class DeleteGymDocumentCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteGymDocumentCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteGymDocumentCommandHandler : IRequestHandler<DeleteGymDocumentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymDocumentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteGymDocumentCommand request, CancellationToken cancellationToken)
    {
        var gymExists = await _unitOfWork.Repository<GymDocument>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!gymExists)
        {
            return Result<bool>.BadRequest("Gym Document not Found");
        }

        await _unitOfWork.Repository<GymDocument>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Gym Document deleted successfully");
    }
}
