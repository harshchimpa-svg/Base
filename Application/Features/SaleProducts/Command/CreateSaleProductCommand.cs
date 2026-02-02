using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.SaleProducts;
using MediatR;
using Shared;

namespace Application.Features.SaleProducts.Command;

public class CreateSaleProductCommand: IRequest<Result<string>>, ICreateMapFrom<SaleProduct>
{
    public int? SaleId { get; set; }
    // public int? ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal taxe { get; set; }
}

internal class CreateSaleProductCommandHandler : IRequestHandler<CreateSaleProductCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSaleProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateSaleProductCommand request, CancellationToken cancellationToken)
    {
        var diet = _mapper.Map<SaleProduct>(request);

        await _unitOfWork.Repository<SaleProduct>().AddAsync(diet);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Diet created successfully.");
    }
}