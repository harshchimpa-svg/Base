using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.PaymentHeades;
using Domain.Entities.PaymentHeates;
using MediatR;
using Shared;

namespace Application.Features.PaymentHeades.Command;

public class CreatePaymentHeadCommand : IRequest<Result<string>>, ICreateMapFrom<PaymentHead>
{
    public string Name { get; set; }
    public PaymentHeadType PaymentHeadType { get; set; }
}
internal class CreatePaymentHeadCommandHandler : IRequestHandler<CreatePaymentHeadCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePaymentHeadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreatePaymentHeadCommand request, CancellationToken cancellationToken)
    {

        var PaymentHead = _mapper.Map<PaymentHead>(request);

        await _unitOfWork.Repository<PaymentHead>().AddAsync(PaymentHead);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("PaymentHead created successfully.");
    }
}