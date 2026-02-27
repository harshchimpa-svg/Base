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

public class GetAllGymTranerQuery: IRequest<Result<List<GetGymTranerDto>>>
{
}
internal class GetAllGymTranerQueryHandler : IRequestHandler<GetAllGymTranerQuery, Result<List<GetGymTranerDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGymTranerQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetGymTranerDto>>> Handle(GetAllGymTranerQuery request, CancellationToken cancellationToken)
    {
        var gymTraners = await _unitOfWork.Repository<GemTraner>().GetAll();

        var map = _mapper.Map<List<GetGymTranerDto>>(gymTraners);

        return Result<List<GetGymTranerDto>>.Success(map, "GemTraner list");
    }
}