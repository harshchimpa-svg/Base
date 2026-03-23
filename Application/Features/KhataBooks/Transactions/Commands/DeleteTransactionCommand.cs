using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Balance.Command;

public class DeleteTransactionCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteTransactionCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteTransactionCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<bool>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            ?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
            return Result<bool>.BadRequest("User is not authenticated");

        var transaction = await _unitOfWork.Repository<Transaction>().Entities
            .Include(t => t.Customer)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.CreatedBy == userId, cancellationToken);

        if (transaction == null)
            return Result<bool>.BadRequest("Transaction not found or not authorized.");

        var customer = transaction.Customer;

        if (transaction.TransactionType == TransactionType.Credit)
            customer.Balance -= transaction.Amount;
        else
            customer.Balance += transaction.Amount;

        await _unitOfWork.Repository<Customer>().UpdateAsync(customer);
        await _unitOfWork.Repository<Transaction>().DeleteAsync(transaction.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Deleted successfully.");
    }
}
