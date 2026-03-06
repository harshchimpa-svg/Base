using Application.Dto.Customers;
using Application.Dto.GymTrainers;
using Application.Features.Customers.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using Domain.Entities.GymTrainers;
using MediatR;
using Shared;

namespace Application.Features.GymTrainers.Queries;

public class GetAllGymTrainerQuery: IRequest<Result<List<GetGymTrainerDto>>>
{
}
internal class GetAllGymTrainerQueryHandler : IRequestHandler<GetAllGymTrainerQuery, Result<List<GetGymTrainerDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGymTrainerQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)      
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetGymTrainerDto>>> Handle(GetAllGymTrainerQuery request, CancellationToken cancellationToken)
    {
        var gymTrainers = await _unitOfWork.Repository<GymTrainer>().GetAll();

        var map = _mapper.Map<List<GetGymTrainerDto>>(gymTrainers);

        return Result<List<GetGymTrainerDto>>.Success(map, "GymTrainer list");
    }
}