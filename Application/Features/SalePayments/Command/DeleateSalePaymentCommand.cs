using Application.Features.SaleProducts.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.SalePayments;
using Domain.Entities.SaleProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SalePayments.Command;

public class DeleateSalePaymentCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateSalePaymentCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateSalePaymentCommandHandler : IRequestHandler<DeleateSalePaymentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateSalePaymentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleateSalePaymentCommand request, CancellationToken cancellationToken)
    {
        var SalePayment = await _unitOfWork.Repository<SalePayment>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!SalePayment)
        {
            return Result<bool>.BadRequest("SalePayments not found.");
        }

        await _unitOfWork.Repository<SalePayment>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "SaleProduct deleted successfully.");
    }
}