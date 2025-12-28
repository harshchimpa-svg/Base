using Application.Dto.Transicstiones;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Transicstions;
using MediatR;
using Shared;

namespace Application.Features.Transicstiones.Queries;

public class GetTransicstionByIdQuery : IRequest<Result<GetTransicstionDto>>
{
    public int Id { get; set; }

    public GetTransicstionByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetTransicstionByIdQueryHandler : IRequestHandler<GetTransicstionByIdQuery, Result<GetTransicstionDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTransicstionByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetTransicstionDto>> Handle(GetTransicstionByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Transicstion>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetTransicstionDto>.BadRequest("Location not found.");
        }

        var mapData = _mapper.Map<GetTransicstionDto>(location);

        return Result<GetTransicstionDto>.Success(mapData, "Location");
    }
}
