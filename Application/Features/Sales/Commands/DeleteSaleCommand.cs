using Application.Features.Diets.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Diets;
using Domain.Entities.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Sales.Command;

public class DeleteSaleCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteSaleCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSaleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var saleExists = await _unitOfWork.Repository<Sale>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!saleExists)
        {
            return Result<bool>.BadRequest("Sale not found.");
        }

        await _unitOfWork.Repository<Sale>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Sale deleted successfully.");
    }
}