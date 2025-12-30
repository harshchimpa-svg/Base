using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Documents.Commands;

public class DeleteDocumentCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteDocumentCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteDocumentCommandHandler
    : IRequestHandler<DeleteDocumentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDocumentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.Repository<Document>()
            .Entities.AnyAsync(x => x.Id == request.Id);

        if (!exists)
        {
            return Result<bool>.BadRequest("Document not found.");
        }

        await _unitOfWork.Repository<Document>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Document deleted successfully.");
    }
}
