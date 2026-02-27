using Application.Dto.SaleProducts;
using Application.Dto.Sales;
using Application.Features.Sales.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.SaleProducts;
using Domain.Entities.Sales;
using MediatR;
using Shared;

namespace Application.Features.SaleProducts.Queries;

public class GetSaleProductByIdQuery: IRequest<Result<GetSaleProductDto>>
{
    public int Id { get; set; }

    public GetSaleProductByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetSaleProductQueryHandler : IRequestHandler<GetSaleProductByIdQuery, Result<GetSaleProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetSaleProductDto>> Handle(GetSaleProductByIdQuery request, CancellationToken cancellationToken)

    {
        var SalePayment = await _unitOfWork.Repository<SaleProduct>().GetByID(request.Id);

        if (SalePayment == null)
        {
            return Result<GetSaleProductDto>.BadRequest("SalePayment not found.");
        }

        var mapData = _mapper.Map<GetSaleProductDto>(SalePayment);

        return Result<GetSaleProductDto>.Success(mapData, "SalePayment");
    }
}