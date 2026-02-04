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
        var Clients = await _unitOfWork.Repository<Sale>().GetByID(request.Id);

        if (Clients == null)
        {
            return Result<GetSaleDto>.BadRequest("Clients not found.");
        }

        var mapData = _mapper.Map<GetSaleDto>(Clients);

        return Result<GetSaleDto>.Success(mapData, "Clients");
    }
}