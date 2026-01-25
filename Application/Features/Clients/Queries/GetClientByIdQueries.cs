using Application.Dto.Clientses;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using MediatR;
using Shared;

namespace Application.Features.Clientses.Queries;

public class GetClientByIdQueries: IRequest<Result<GetClientsDto>>
{
    public int Id { get; set; }

    public GetClientByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetClientsByIdQueriesHandler : IRequestHandler<GetClientByIdQueries, Result<GetClientsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetClientsDto>> Handle(GetClientByIdQueries request, CancellationToken cancellationToken)

    {
        var Clients = await _unitOfWork.Repository<Clients>().GetByID(request.Id);

        if (Clients == null)
        {
            return Result<GetClientsDto>.BadRequest("Clients not found.");
        }

        var mapData = _mapper.Map<GetClientsDto>(Clients);

        return Result<GetClientsDto>.Success(mapData, "Clients");
    }
}