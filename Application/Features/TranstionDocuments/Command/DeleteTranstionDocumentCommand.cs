using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.TranstionDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.TranstionDocuments.Command;

public class DeleteTranstionDocumentCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteTranstionDocumentCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteTranstionDocumentCommandHandler : IRequestHandler<DeleteTranstionDocumentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTranstionDocumentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteTranstionDocumentCommand request, CancellationToken cancellationToken)
    {
        var ChairExists = await _unitOfWork.Repository<TranstionDocument>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!ChairExists)
        {
            return Result<bool>.BadRequest("Chair not found.");
        }

        await _unitOfWork.Repository<TranstionDocument>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Chair deleted successfully.");
    }
}