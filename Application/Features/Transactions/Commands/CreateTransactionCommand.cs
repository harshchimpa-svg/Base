using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
using Domain.Entities.Transactions;
using Domain.Entities.PaymentLogs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Command;

public class CreateTransactionCommand : IRequest<Result<string>>, ICreateMapFrom<Transaction>
{
    [Required]
    public int CustomerId { get; set; }

    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
}

internal class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<string>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            ?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
            return Result<string>.BadRequest("User is not authenticated");

        var customer = await _unitOfWork
            .Repository<Customer>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId);

        if (customer == null)
            return Result<string>.BadRequest("Customer not found");

        var transaction = _mapper.Map<Transaction>(request);
        transaction.UserId = userId;  
        await _unitOfWork.Repository<Transaction>().AddAsync(transaction);

        if (transaction.TransactionType == TransactionType.Credit)
            customer.Balance += transaction.Amount;
        else
            customer.Balance -= transaction.Amount;

        await _unitOfWork.Repository<Customer>().UpdateAsync(customer);

        var paymentLoge = new PaymentLog
        {
            UserId = userId, 
            CustomerId = customer.Id,
            Transaction = transaction,
            TransactionId = transaction.Id,
            Amount = transaction.Amount,
            TransactionType = transaction.TransactionType,
            CreatedDate = DateTime.UtcNow
        };

        await _unitOfWork.Repository<PaymentLog>().AddAsync(paymentLoge);

        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Transaction created successfully");
    }
}