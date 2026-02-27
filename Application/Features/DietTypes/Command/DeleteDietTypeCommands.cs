using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.DietTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DietTypes.Command;

public class DeleteDietTypeCommands: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteDietTypeCommands(int id)
    {
        Id = id;
    }
}
internal class DeleteDietTypeCommandsHandler : IRequestHandler<DeleteDietTypeCommands, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDietTypeCommandsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteDietTypeCommands request, CancellationToken cancellationToken) 
    {
        var dietTypesExists = await _unitOfWork.Repository<DietType>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!dietTypesExists)
        {
            return Result<bool>.BadRequest("DietType not found.");
        }

        await _unitOfWork.Repository<DietType>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "DietType deleted successfully.");
    }
}