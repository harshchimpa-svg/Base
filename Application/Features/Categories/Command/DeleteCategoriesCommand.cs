using Application.Interfaces.UnitOfWorkRepositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Categoryes.Command;

public class DeleteCategoriesCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteCategoriesCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoriesCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteCategoriesCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _unitOfWork.Repository<Category>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!categoryExists)
        {
            return Result<bool>.BadRequest("Category not found.");
        }

        await _unitOfWork.Repository<Category>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Category deleted successfully.");
    }
}