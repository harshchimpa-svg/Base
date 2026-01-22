
using Application.Dto.Gyms;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.Gyms.Queries;

public class GetByIdGymQuery : IRequest<Result<GetGymDto>>
{
    public int Id { get; set; }

    public GetByIdGymQuery(int id)
    {
        Id = id;
    }
}

internal class GetByIdGymQueryHandler : IRequestHandler<GetByIdGymQuery, Result<GetGymDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdGymQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetGymDto>> Handle(GetByIdGymQuery request, CancellationToken cancellationToken)
    {
        var gym = await _unitOfWork.Repository<Gym>().GetByID(request.Id);

        if (gym == null)
        {
            return Result<GetGymDto>.BadRequest("GYm Not Found");
        }

        var mapData = _mapper.Map<GetGymDto>(gym);

        return Result<GetGymDto>.Success(mapData, "Gym");
    }
}
