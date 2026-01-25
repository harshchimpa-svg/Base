using Application.Interfaces.UnitOfWorkRepositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Categoryes.Command;

public class DeleteCategoriCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteCategoriCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCategoriCommandHandler : IRequestHandler<DeleteCategoriCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoriCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteCategoriCommand request, CancellationToken cancellationToken)
    {
        var CategoriExists = await _unitOfWork.Repository<Category>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!CategoriExists)
        {
            return Result<bool>.BadRequest("Categori not found.");
        }

        await _unitOfWork.Repository<Category>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Categori deleted successfully.");
    }
}