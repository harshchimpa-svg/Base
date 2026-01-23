using Application.Dto.diteTypes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DiteTypes;
using MediatR;
using Shared;

namespace Application.Features.DiteTypes.Queries;

public class GetAllDiteTypeQuery: IRequest<Result<List<GetDiteTypeDto>>>
{
}
internal class GetAllDiteTypeQueryHandler : IRequestHandler<GetAllDiteTypeQuery, Result<List<GetDiteTypeDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDiteTypeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<GetDiteTypeDto>>> Handle(GetAllDiteTypeQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<DiteType>().GetAll();

        var map = _mapper.Map<List<GetDiteTypeDto>>(locations);

        return Result<List<GetDiteTypeDto>>.Success(map, "Location list");
    }
}