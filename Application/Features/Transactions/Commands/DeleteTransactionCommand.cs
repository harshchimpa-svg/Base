using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
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
internal class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken) 
    {
        var transaction = await _unitOfWork.Repository<Transaction>().Entities
            .Include(t => t.Customer)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (transaction == null)
        {
            return Result<bool>.BadRequest("Transaction not found.");
        }

        var customer = transaction.Customer;
        if (customer == null)
        {
            return Result<bool>.BadRequest("Customer not found ");
        }

        if (transaction.TransactionType == TransactionType.Credit)
            customer.Balance =customer.Balance + transaction.Amount; 
        else
            customer.Balance = customer.Balance - transaction.Amount;

        await _unitOfWork.Repository<Customer>().UpdateAsync(customer);

        await _unitOfWork.Repository<Transaction>().DeleteAsync(transaction.Id);

        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Deleted successfully.");
    }
}
