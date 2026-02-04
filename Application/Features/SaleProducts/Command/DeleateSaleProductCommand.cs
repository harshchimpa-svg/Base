using Application.Features.Sales.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.SaleProducts;
using Domain.Entities.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using SaleProduct = Domain.Entities.SaleProducts.SaleProduct;

namespace Application.Features.SaleProducts.Command;

public class DeleateSaleProductCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateSaleProductCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateSaleProductCommandHandler : IRequestHandler<DeleateSaleProductCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateSaleProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleateSaleProductCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<SaleProduct>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Diets not found.");
        }

        await _unitOfWork.Repository<SaleProduct>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "SaleProduct deleted successfully.");
    }
}