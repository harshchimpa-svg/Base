using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Dites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dites.Commands;

public class DeleateDiteCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateDiteCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateDiteCommandHandler : IRequestHandler<DeleateDiteCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateDiteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleateDiteCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<diet>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Dites not found.");
        }

        await _unitOfWork.Repository<diet>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Dite deleted successfully.");
    }
}