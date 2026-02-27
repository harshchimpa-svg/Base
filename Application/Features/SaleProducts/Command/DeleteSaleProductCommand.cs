using Application.Features.Sales.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.SaleProducts;
using Domain.Entities.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using SaleProduct = Domain.Entities.SaleProducts.SaleProduct;

namespace Application.Features.SaleProducts.Command;

public class DeleteSaleProductCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteSaleProductCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteSaleProductCommandHandler : IRequestHandler<DeleteSaleProductCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSaleProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteSaleProductCommand request, CancellationToken cancellationToken)
    {
        var salePayment = await _unitOfWork.Repository<SaleProduct>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!salePayment)
        {
            return Result<bool>.BadRequest("Diets not found.");
        }

        await _unitOfWork.Repository<SaleProduct>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "SaleProduct deleted successfully.");
    }
}