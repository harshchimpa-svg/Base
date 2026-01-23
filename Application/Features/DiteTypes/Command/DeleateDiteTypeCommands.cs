using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.DiteTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DiteTypes.Command;

public class DeleateDiteTypeCommands: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateDiteTypeCommands(int id)
    {
        Id = id;
    }
}
internal class DeleateDiteTypeCommandsHandler : IRequestHandler<DeleateDiteTypeCommands, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateDiteTypeCommandsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateDiteTypeCommands request, CancellationToken cancellationToken) 
    {
        var locationExists = await _unitOfWork.Repository<DiteType>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Catgory not found.");
        }

        await _unitOfWork.Repository<DiteType>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Balance deleted successfully.");
    }
}