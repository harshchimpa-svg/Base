

using Application.Dto.Gyms;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Gyms;
using MediatR;
using Shared;


namespace Application.Features.Gyms.Queries;

public class GetGymQuery : IRequest<Result<List<GetGymDto>>>
{

}

internal class GetGymQueryHandler : IRequestHandler<GetGymQuery, Result<List<GetGymDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetGymQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetGymDto>>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
       var gyms =await _unitOfWork.Repository<Gym>().GetAll();

        var map = _mapper.Map<List<GetGymDto>>(gyms);

        return Result<List<GetGymDto>>.Success(map, "Gym List");
    }
}