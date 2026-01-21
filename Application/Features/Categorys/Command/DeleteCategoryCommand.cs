using Application.Interfaces.UnitOfWorkRepositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagoryes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Catgoryes.Command;

public class DeleteCategoryCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var CategoryExists = await _unitOfWork.Repository<Category>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!CategoryExists)
        {
            return Result<bool>.BadRequest("Category not found.");
        }

        await _unitOfWork.Repository<Category>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Category deleted successfully.");
    }
}