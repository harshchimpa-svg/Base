using Application.Dto.Transicstiones;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Transicstions;
using MediatR;
using Shared;

namespace Application.Features.Transicstiones.Queries;

public class GetAllTransicstionQuery : IRequest<Result<List<GetTransicstionDto>>>
{
}
internal class GetAllTransicstionQueryHandler : IRequestHandler<GetAllTransicstionQuery, Result<List<GetTransicstionDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTransicstionQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<GetTransicstionDto>>> Handle(GetAllTransicstionQuery request, CancellationToken cancellationToken)
    {
        var locations = _unitOfWork.Repository<Transicstion>().Entities.AsQueryable();

        var map = _mapper.Map<List<GetTransicstionDto>>(locations);

        return Result<List<GetTransicstionDto>>.Success(map, "Location list");
    }
}