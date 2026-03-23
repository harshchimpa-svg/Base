using Application.Features.SaleProducts.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.SalePayments;
using Domain.Entities.SaleProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.SalePayments.Command;

public class DeleteSalePaymentCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteSalePaymentCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteSalePaymentCommandHandler : IRequestHandler<DeleteSalePaymentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSalePaymentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteSalePaymentCommand request, CancellationToken cancellationToken)
    {
        var salePayment = await _unitOfWork.Repository<SalePayment>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!salePayment)
        {
            return Result<bool>.BadRequest("SalePayment not found.");
        }

        await _unitOfWork.Repository<SalePayment>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "SalePayment deleted successfully.");
    }
}