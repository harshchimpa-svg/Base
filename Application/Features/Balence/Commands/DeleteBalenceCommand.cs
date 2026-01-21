using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Balances;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Commands;

public class DeleteBalenceCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteBalenceCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteBalenceCommandHandler : IRequestHandler<DeleteBalenceCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBalenceCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteBalenceCommand request, CancellationToken cancellationToken) 
    {
        var locationExists = await _unitOfWork.Repository<Balance>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Catgory not found.");
        }

        await _unitOfWork.Repository<Balance>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Balance deleted successfully.");
    }
}