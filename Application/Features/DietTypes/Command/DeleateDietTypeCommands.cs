using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.DietTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DietTypes.Command;

public class DeleateDietTypeCommands: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateDietTypeCommands(int id)
    {
        Id = id;
    }
}
internal class DeleateDietTypeCommandsHandler : IRequestHandler<DeleateDietTypeCommands, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateDietTypeCommandsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateDietTypeCommands request, CancellationToken cancellationToken) 
    {
        var locationExists = await _unitOfWork.Repository<DietType>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("DietType not found.");
        }

        await _unitOfWork.Repository<DietType>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "DietType deleted successfully.");
    }
}