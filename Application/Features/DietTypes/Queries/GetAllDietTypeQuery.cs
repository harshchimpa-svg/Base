using Application.Dto.dietTypes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietTypes;
using MediatR;
using Shared;

namespace Application.Features.DietTypes.Queries;

public class GetAllDietTypeQuery: IRequest<Result<List<GetDietTypeDto>>>
{
}
internal class GetAllDietTypeQueryHandler : IRequestHandler<GetAllDietTypeQuery, Result<List<GetDietTypeDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDietTypeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<GetDietTypeDto>>> Handle(GetAllDietTypeQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<DietType>().GetAll();

        var map = _mapper.Map<List<GetDietTypeDto>>(locations);

        return Result<List<GetDietTypeDto>>.Success(map, "Location list");
    }
}