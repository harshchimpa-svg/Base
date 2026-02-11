using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
using Domain.Entities.Transactions;
using Domain.Entities.PaymentLoges;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Commands;

public class CreateTransactionCommand 
    : IRequest<Result<string>>, ICreateMapFrom<Transaction>
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public string UserId { get; set; }

    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
}

internal class CreateBalenceCommandHandler : IRequestHandler<CreateTransactionCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBalenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserId))
            return Result<string>.BadRequest("UserId is required");

        var customer = await _unitOfWork
            .Repository<Customer>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer == null)
            return Result<string>.BadRequest("Customer not found");

        var transaction = _mapper.Map<Transaction>(request);
        await _unitOfWork.Repository<Transaction>().AddAsync(transaction);

        if (transaction.TransactionType == TransactionType.Credit)
            customer.Balance += transaction.Amount;
        else
            customer.Balance -= transaction.Amount;

        await _unitOfWork.Repository<Customer>().UpdateAsync(customer);

        var paymentLoge = new PaymentLoge
        {
            UserId = request.UserId,
            CustomerId = customer.Id,
            Transaction = transaction,
            TransactionId = transaction.Id,
            Amount = transaction.Amount,
            TransactionType = transaction.TransactionType,
            CreatedDate = DateTime.UtcNow
        };

        await _unitOfWork.Repository<PaymentLoge>().AddAsync(paymentLoge);

        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Transactioncreated successfully");
    }
}
