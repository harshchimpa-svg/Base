using Application.Dto.Clientses;
using Application.Features.Services.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using MediatR;
using Shared;

namespace Application.Features.Clientses.Queries;

public class GetAllClientsQueries: IRequest<Result<List<GetClientsDto>>>
{
}
internal class GetAllClientsQueriesHandler : IRequestHandler<GetAllClientsQueries, Result<List<GetClientsDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllClientsQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<List<GetClientsDto>>> Handle(GetAllClientsQueries request, CancellationToken cancellationToken)
    {
        var Clients = await _unitOfWork.Repository<Clients>().GetAll();

        var map = _mapper.Map<List<GetClientsDto>>(Clients);

        return Result<List<GetClientsDto>>.Success(map, "Clients list");
    }
}