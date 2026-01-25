using Application.Dto.Clientses;
using Application.Dto.Diets;
using Application.Features.Clientses.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using Domain.Entities.Diets;
using MediatR;
using Shared;

namespace Application.Features.Diets.Queries;

public class GetDietByIdQueries: IRequest<Result<GetDietDto>>
{
    public int Id { get; set; }

    public GetDietByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetDietByIdQueriesHandler : IRequestHandler<GetDietByIdQueries, Result<GetDietDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDietByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetDietDto>> Handle(GetDietByIdQueries request, CancellationToken cancellationToken)

    {
        var Clients = await _unitOfWork.Repository<Diet>().GetByID(request.Id);

        if (Clients == null)
        {
            return Result<GetDietDto>.BadRequest("Clients not found.");
        }

        var mapData = _mapper.Map<GetDietDto>(Clients);

        return Result<GetDietDto>.Success(mapData, "Clients");
    }
}