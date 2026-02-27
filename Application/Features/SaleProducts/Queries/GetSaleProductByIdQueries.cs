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

public class GetSaleProductByIdQueries: IRequest<Result<GetSaleProductDto>>
{
    public int Id { get; set; }

    public GetSaleProductByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetSaleProductQueriesHandler : IRequestHandler<GetSaleProductByIdQueries, Result<GetSaleProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleProductQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetSaleProductDto>> Handle(GetSaleProductByIdQueries request, CancellationToken cancellationToken)

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