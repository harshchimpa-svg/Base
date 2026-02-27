using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Diets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Diets.Commands;

public class DeleteDietCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteDietCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteDietCommandHandler : IRequestHandler<DeleteDietCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDietCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteDietCommand request, CancellationToken cancellationToken)
    {
        var DietsExists = await _unitOfWork.Repository<Diet>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!DietsExists)
        {
            return Result<bool>.BadRequest("Diets not found.");
        }

        await _unitOfWork.Repository<Diet>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Diet deleted successfully.");
    }
}