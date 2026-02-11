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

public class GetSalePaymentByIdQueries: IRequest<Result<GetSalePaymentDto>>
{
    public int Id { get; set; }

    public GetSalePaymentByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetSalePaymentByIdQueriesHandler : IRequestHandler<GetSalePaymentByIdQueries, Result<GetSalePaymentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSalePaymentByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetSalePaymentDto>> Handle(GetSalePaymentByIdQueries request, CancellationToken cancellationToken)

    {
        var Clients = await _unitOfWork.Repository<SalePayment>().GetByID(request.Id);

        if (Clients == null)
        {
            return Result<GetSalePaymentDto>.BadRequest("Clients not found.");
        }

        var mapData = _mapper.Map<GetSalePaymentDto>(Clients);

        return Result<GetSalePaymentDto>.Success(mapData, "Clients");
    }
}