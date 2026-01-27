using Application.Dto.dietTypes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietTypes;
using MediatR;
using Shared;

namespace Application.Features.DietTypes.Queries;

public class GetDietTypeByIdQuery: IRequest<Result<GetDietTypeDto>>
{
    public int Id { get; set; }

    public GetDietTypeByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetDietTypeByIdQueryHandler : IRequestHandler<GetDietTypeByIdQuery, Result<GetDietTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDietTypeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<GetDietTypeDto>> Handle(GetDietTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<DietType>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetDietTypeDto>.BadRequest("DietType not found.");
        }

        var mapData = _mapper.Map<GetDietTypeDto>(location);

        return Result<GetDietTypeDto>.Success(mapData, "DietType");
    }
}