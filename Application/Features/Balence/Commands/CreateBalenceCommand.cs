using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.BalanceTypes;
using Domain.Entities.Balances;
using Domain.Entities.PaymentLoges;
using MediatR;
using Shared;

namespace Application.Features.Balence.Commands;

public class CreateBalenceCommand : IRequest<Result<string>>, ICreateMapFrom<Balance>
{
    public int? CustomerId { get; set; }
    public string UserId { get; set; }  
    public BalanceType BalanceType { get; set; }
    public decimal Amount { get; set; }
}

internal class CreateBalenceCommandHandler : IRequestHandler<CreateBalenceCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBalenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateBalenceCommand request, CancellationToken cancellationToken)
    {
        if (request.CustomerId == null)
            return Result<string>.BadRequest("CustomerId is required");

        if (string.IsNullOrEmpty(request.UserId))
            return Result<string>.BadRequest("UserId is required");

        var balance = _mapper.Map<Balance>(request);
        await _unitOfWork.Repository<Balance>().AddAsync(balance);
        await _unitOfWork.Save(cancellationToken);

        var paymentLoge = new PaymentLoge
        {
            Date = DateTime.UtcNow,
            Amount = request.Amount,
            BalanceType = request.BalanceType,
            UserId = request.UserId,
            CustomerId = request.CustomerId.Value,
            BalanceId = balance.Id
        };

        await _unitOfWork.Repository<PaymentLoge>().AddAsync(paymentLoge);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Created successfully.");
    }
}
