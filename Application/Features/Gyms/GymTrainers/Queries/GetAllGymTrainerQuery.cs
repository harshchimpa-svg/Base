using Application.Dto.Customers;
using Application.Dto.GymTraners;
using Application.Features.Customers.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using Domain.Entities.GymTraners;
using MediatR;
using Shared;

namespace Application.Features.GymTraners.Queries;

public class GetAllGymTrainerQuery: IRequest<Result<List<GetGymTrainerDto>>>
{
}
internal class GetAllGymTranerQueryHandler : IRequestHandler<GetAllGymTrainerQuery, Result<List<GetGymTrainerDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGymTranerQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)      
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetGymTrainerDto>>> Handle(GetAllGymTrainerQuery request, CancellationToken cancellationToken)
    {
        var gymTraners = await _unitOfWork.Repository<GymTrainer>().GetAll();

        var map = _mapper.Map<List<GetGymTrainerDto>>(gymTraners);

        return Result<List<GetGymTrainerDto>>.Success(map, "GymTrainer list");
    }
}