
using Application.Dto.Locations;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Locations;
using MediatR;
using Shared;

namespace Application.Features.Locations.Queries;

public class GetAllLocationQuery : IRequest<Result<List<GetLocationDto>>>
{
}

internal class GetLocationQueryHandler : IRequestHandler<GetAllLocationQuery, Result<List<GetLocationDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetLocationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
         _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetLocationDto>>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Location>().GetAll();

        var map = _mapper.Map<List<GetLocationDto>>(locations);

        return Result<List<GetLocationDto>>.Success(map, "Location List");
    }
}