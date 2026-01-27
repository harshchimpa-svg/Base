using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Diets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Diets.Commands;

public class DeleateDietCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateDietCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateDietCommandHandler : IRequestHandler<DeleateDietCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateDietCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleateDietCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Diet>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Diets not found.");
        }

        await _unitOfWork.Repository<Diet>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Diet deleted successfully.");
    }
}