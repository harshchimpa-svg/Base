using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.SalePayments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SalePayments.Command;

public class UpdateSalePaymentCommand: IRequest<Result<SalePayment>>
{

    public int Id { get; set; }
    public CreateSalePaymentsCommand CreateCommand { get; set; } = new();

    public UpdateSalePaymentCommand(int id, CreateSalePaymentsCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateSalePaymentCommandHandler : IRequestHandler<UpdateSalePaymentCommand, Result<SalePayment>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSalePaymentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<SalePayment>> Handle(UpdateSalePaymentCommand request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<SalePayment>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (location == null)
        {
            return Result<SalePayment>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, location);

        await _unitOfWork.Repository<SalePayment>().UpdateAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<SalePayment>.Success("Update SaleProduct...");
    }
}