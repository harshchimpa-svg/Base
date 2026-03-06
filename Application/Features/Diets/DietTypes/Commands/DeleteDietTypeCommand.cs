using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.DietTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DietTypes.Command;

public class DeleteDietTypeCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteDietTypeCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteDietTypeCommandsHandler : IRequestHandler<DeleteDietTypeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDietTypeCommandsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteDietTypeCommand request, CancellationToken cancellationToken) 
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