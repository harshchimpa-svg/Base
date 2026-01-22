

using Application.Dto.Locations;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Locations;
using MediatR;
using Shared;

namespace Application.Features.Locations.Queries;

public class GetByIdLocationQuery : IRequest<Result<GetLocationDto>>
{
    public int Id { get; set; }


    public GetByIdLocationQuery(int id)
    {
        Id = id;
    }
}

internal class GetByIdLocationQueryHandler : IRequestHandler<GetByIdLocationQuery, Result<GetLocationDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdLocationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetLocationDto>> Handle(GetByIdLocationQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Location>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetLocationDto>.BadRequest("Location not found");
        }

        var mapData = _mapper.Map<GetLocationDto>(location);

        return Result<GetLocationDto>.Success(mapData, "Location");
    }
}