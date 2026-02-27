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

public class GetSaleByIdQueries: IRequest<Result<GetSaleDto>>
{
    public int Id { get; set; }

    public GetSaleByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetSaleByIdQueriesHandler : IRequestHandler<GetSaleByIdQueries, Result<GetSaleDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetSaleDto>> Handle(GetSaleByIdQueries request, CancellationToken cancellationToken)

    {
        var Sale = await _unitOfWork.Repository<Sale>().GetByID(request.Id);

        if (Sale == null)
        {
            return Result<GetSaleDto>.BadRequest("Sale not found.");
        }

        var mapData = _mapper.Map<GetSaleDto>(Sale);

        return Result<GetSaleDto>.Success(mapData, "Sale");
    }
}