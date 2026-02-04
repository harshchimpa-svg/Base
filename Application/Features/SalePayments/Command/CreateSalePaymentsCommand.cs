using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Common.Enums.Status;
using Domain.Entities.SalePayments;
using Domain.Entities.Sales;
using MediatR;
using Shared;

namespace Application.Features.SalePayments.Command;

public class CreateSalePaymentsCommand: IRequest<Result<string>>, ICreateMapFrom<SalePayment>
{
    public int? SaleId { get; set; }
    public MethodType MethodType { get; set; }
    public decimal NetAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public StatusType StatusType  { get; set; }
}

internal class CreateSalePaymentsCommandHandler : IRequestHandler<CreateSalePaymentsCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSalePaymentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateSalePaymentsCommand request, CancellationToken cancellationToken)
    {
        if (request.SaleId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<Sale>().GetByID(request.SaleId.Value);
            if (parentExists == null)
            {
                return Result<string>.BadRequest("SaleId not exit");
            }
        }
        var diet = _mapper.Map<SalePayment>(request);

        await _unitOfWork.Repository<SalePayment>().AddAsync(diet);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("SalePayment created successfully.");
    }
}