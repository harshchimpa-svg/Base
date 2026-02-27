using Application.Dto.Clientses;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using MediatR;
using Shared;

namespace Application.Features.Clients.Queries;

public class GetClientByIdQuery: IRequest<Result<GetClientsDto>>
{
    public int Id { get; set; }

    public GetClientByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetClientsByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Result<GetClientsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetClientsDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)

    {
        var clients = await _unitOfWork.Repository<Client>().GetByID(request.Id);

        if (clients == null)
        {
            return Result<GetClientsDto>.BadRequest("Clients not found.");
        }

        var mapData = _mapper.Map<GetClientsDto>(clients);

        return Result<GetClientsDto>.Success(mapData, "Clients");
    }
}