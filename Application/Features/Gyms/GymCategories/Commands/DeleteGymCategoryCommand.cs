

using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymCategorys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymCategories.Command;

public class DeleteGymCategoryCommand : IRequest<Result<string>>
{
  public int Id { get; set; }
  public  DeleteGymCategoryCommand(int id)
  {
    Id = id;
  }

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
        var gymCategory = await _unitOfWork.Repository<Domain.Entities.GymCategorys.GymCategories>()
            .Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (gymCategory == null)
            return Result<string>.BadRequest("Gym Category not found");

        await _unitOfWork.Repository<Domain.Entities.GymCategorys.GymCategories>().DeleteAsync(gymCategory);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym Category Deleted");
    }
}
