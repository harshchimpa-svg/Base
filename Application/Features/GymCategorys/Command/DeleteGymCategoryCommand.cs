

using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymCategorys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymCategorys.Command;

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
        var category = await _unitOfWork.Repository<GymCategory>()
            .Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (category == null)
            return Result<string>.BadRequest("Gym Category not found");

        await _unitOfWork.Repository<GymCategory>().DeleteAsync(category);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym Category Deleted");
    }
}
