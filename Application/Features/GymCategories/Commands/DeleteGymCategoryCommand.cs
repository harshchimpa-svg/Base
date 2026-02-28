

using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymCategorys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymCategories.Command;

public class DeleteGymCategoryCommand : IRequest<Result<string>>
{
  public int Id { get; set; }

}
internal class DeleteGymCategoryCommandHandler : IRequestHandler<DeleteGymCategoryCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle( DeleteGymCategoryCommand request, CancellationToken cancellationToken)
    {
        var gymCategory = await _unitOfWork.Repository<GymCategory>()
            .Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (gymCategory == null)
            return Result<string>.BadRequest("Gym Category not found");

        await _unitOfWork.Repository<GymCategory>().DeleteAsync(gymCategory);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym Category Deleted");
    }
}
