using Application.Features.Sales.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.SaleProducts;
using Domain.Entities.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using SaleProduct = Domain.Entities.SaleProducts.SaleProduct;

namespace Application.Features.SaleProducts.Command;

public class UpdateSaleProductCommand: IRequest<Result<SaleProduct>>
{

    public int Id { get; set; }
    public CreateSaleProductCommand CreateCommand { get; set; } = new();

    public UpdateSaleProductCommand(int id, CreateSaleProductCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateSaleProductCommandHandler : IRequestHandler<UpdateSaleProductCommand, Result<SaleProduct>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSaleProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<SaleProduct>> Handle(UpdateSaleProductCommand request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<SaleProduct>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (location == null)
        {
            return Result<SaleProduct>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, location);

        await _unitOfWork.Repository<SaleProduct>().UpdateAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<SaleProduct>.Success("Update SaleProduct...");
    }
}