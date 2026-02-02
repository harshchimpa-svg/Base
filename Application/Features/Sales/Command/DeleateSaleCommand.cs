using Application.Features.Diets.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Diets;
using Domain.Entities.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Sales.Command;

public class DeleateSaleCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateSaleCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateSaleCommandHandler : IRequestHandler<DeleateSaleCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateSaleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleateSaleCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Sale>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Diets not found.");
        }

        await _unitOfWork.Repository<Sale>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Sale deleted successfully.");
    }
}