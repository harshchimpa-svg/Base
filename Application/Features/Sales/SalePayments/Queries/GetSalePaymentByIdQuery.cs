using Application.Dto.SalePayments;
using Application.Dto.SaleProducts;
using Application.Features.SaleProducts.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.SalePayments;
using Domain.Entities.SaleProducts;
using MediatR;
using Shared;

namespace Application.Features.SalePayments.Queries;

public class GetSalePaymentByIdQuery: IRequest<Result<GetSalePaymentDto>>
{
    public int Id { get; set; }

    public GetSalePaymentByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetSalePaymentByIdQueryHandler : IRequestHandler<GetSalePaymentByIdQuery, Result<GetSalePaymentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSalePaymentByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetSalePaymentDto>> Handle(GetSalePaymentByIdQuery request, CancellationToken cancellationToken)

    {
        var salePayment = await _unitOfWork.Repository<SalePayment>().GetByID(request.Id);

        if (salePayment == null)
        {
            return Result<GetSalePaymentDto>.BadRequest("SalePayment not found.");
        }

        var mapData = _mapper.Map<GetSalePaymentDto>(salePayment);

        return Result<GetSalePaymentDto>.Success(mapData, "SalePayment");
    }
}