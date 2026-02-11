using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Commands;

public class UpdateTransactionCommand : IRequest<Result<Transaction>>
{
    public int Id { get; set; }
    public CreateTransactionCommand CreateCommand { get; set; }

    public UpdateTransactionCommand(int id, CreateTransactionCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand ;
    }
}

internal class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Result<Transaction>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransactionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Transaction>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var oldTransaction = await _unitOfWork.Repository<Transaction>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (oldTransaction == null)
            return Result<Transaction>.BadRequest("Transaction ID not found");

        var customer = await _unitOfWork.Repository<Customer>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == oldTransaction.CustomerId, cancellationToken);

        if (customer == null)
            return Result<Transaction>.BadRequest("Customer not found");

        if (oldTransaction.TransactionType == TransactionType.Credit)
            customer.Balance -= oldTransaction.Amount;
        else
            customer.Balance += oldTransaction.Amount;

        _mapper.Map(request.CreateCommand, oldTransaction);

        if (oldTransaction.TransactionType == TransactionType.Credit)
            customer.Balance += oldTransaction.Amount;
        else
            customer.Balance -= oldTransaction.Amount;

        await _unitOfWork.Repository<Transaction>().UpdateAsync(oldTransaction);
        await _unitOfWork.Repository<Customer>().UpdateAsync(customer);
        
        await _unitOfWork.Save(cancellationToken);

        return Result<Transaction>.Success(oldTransaction, "Transaction updated successfully");
    }
}
