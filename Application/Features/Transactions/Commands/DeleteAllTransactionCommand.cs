using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balance.Command;

public class DeleteAllTransactionCommand : IRequest<Result<bool>>
{
    public int CustomerId { get; set; }

    public DeleteAllTransactionCommand(int customerId)
    {
        CustomerId = customerId;
    }
}

internal class DeleteAllTransactionCommandHandler : IRequestHandler<DeleteAllTransactionCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAllTransactionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteAllTransactionCommand request, CancellationToken cancellationToken)
    {
        var transactions = await _unitOfWork.Repository<Transaction>().Entities
            .Where(x => x.CustomerId == request.CustomerId)
            .ToListAsync(cancellationToken);

        if (!transactions.Any())
        {
            return Result<bool>.BadRequest("transation is not found");
        }

        foreach (var transaction in transactions)
        {
            await _unitOfWork.Repository<Transaction>().DeleteAsync(transaction);
        }

        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "deleted successfully");
    }
}