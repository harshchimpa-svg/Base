using Application.Dto.Diets;
using Application.Dto.Sales;
using Application.Features.Diets.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Diets;
using Domain.Entities.Sales;
using MediatR;
using Shared;

namespace Application.Features.Sales.Queries;

public class GetSaleByIdQuery: IRequest<Result<GetSaleDto>>
{
    public int Id { get; set; }

    public GetSaleByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Result<GetSaleDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetSaleDto>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)

    {
        var sale = await _unitOfWork.Repository<Sale>().GetByID(request.Id);

        if (sale == null)
        {
            return Result<GetSaleDto>.BadRequest("Sale not found.");
        }

        var mapData = _mapper.Map<GetSaleDto>(sale);

        return Result<GetSaleDto>.Success(mapData, "Sale");
    }
}