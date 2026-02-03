using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Transactions;
using Domain.Entities.PaymentLoges;
using MediatR;
using Shared;

namespace Application.Features.Balence.Commands;

public class CreateTransactionCommand : IRequest<Result<string>>, ICreateMapFrom<Transaction>
{
    public int? CustomerId { get; set; }
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
        if (request.CustomerId == null)
            return Result<string>.BadRequest("CustomerId is required");

        if (string.IsNullOrEmpty(request.UserId))
            return Result<string>.BadRequest("UserId is required");

        var transaction = _mapper.Map<Transaction>(request);
        await _unitOfWork.Repository<Transaction>().AddAsync(transaction);
        await _unitOfWork.Save(cancellationToken);

        var paymentLoge = new PaymentLoge
        {
            Date = DateTime.UtcNow,
            Amount = request.Amount,
            TransactionType = request.TransactionType,
            UserId = request.UserId,
            CustomerId = request.CustomerId.Value,
            TransactionId = transaction.Id
        };

        await _unitOfWork.Repository<PaymentLoge>().AddAsync(paymentLoge);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Created successfully.");
    }
}
