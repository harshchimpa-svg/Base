using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Commands;

public class DeleteTransactionCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteTransactionCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteBalenceCommandHandler : IRequestHandler<DeleteTransactionCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBalenceCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken) 
    {
        var locationExists = await _unitOfWork.Repository<Transaction>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Catgory not found.");
        }

        await _unitOfWork.Repository<Transaction>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Transaction deleted successfully.");
    }
}