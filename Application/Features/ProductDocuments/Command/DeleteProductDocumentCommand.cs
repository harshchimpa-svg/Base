

using Application.Features.GymDocuments.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymDocuments;
using Domain.Entities.ProductDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ProductDocuments.Command;

public class DeleteProductDocumentCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteProductDocumentCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteProductDocumentCommandHandler : IRequestHandler<DeleteProductDocumentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductDocumentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteProductDocumentCommand request, CancellationToken cancellationToken)
    {
        var gymExists = await _unitOfWork.Repository<ProductDocument>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!gymExists)
        {
            return Result<bool>.BadRequest("Product Document not Found");
        }

        await _unitOfWork.Repository<ProductDocument>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Product Document deleted successfully");
    }
}

