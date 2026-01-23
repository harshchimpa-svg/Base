using Application.Dto.Clientses;
using Application.Dto.Dites;
using Application.Features.Clientses.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using Domain.Entities.Dites;
using MediatR;
using Shared;

namespace Application.Features.Dites.Queries;

public class GetDiteByIdQueries: IRequest<Result<GetDietDto>>
{
    public int Id { get; set; }

    public GetDiteByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetDiteByIdQueriesHandler : IRequestHandler<GetDiteByIdQueries, Result<GetDietDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDiteByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetDietDto>> Handle(GetDiteByIdQueries request, CancellationToken cancellationToken)

    {
        var Clients = await _unitOfWork.Repository<diet>().GetByID(request.Id);

        if (Clients == null)
        {
            return Result<GetDietDto>.BadRequest("Clients not found.");
        }

        var mapData = _mapper.Map<GetDietDto>(Clients);

        return Result<GetDietDto>.Success(mapData, "Clients");
    }
}