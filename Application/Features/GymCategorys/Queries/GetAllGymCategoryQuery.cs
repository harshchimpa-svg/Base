

using Application.Dto.GymCategorys;
using Application.Dto.Gyms;
using Application.Features.Gyms.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCategorys;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.GymCategorys.Queries;

public class GetAllGymCategoryQuery : IRequest<Result<List<GetGymCategoryDto>>>
{

}
internal class GetAllGymCategoryQueryHandler : IRequestHandler<GetAllGymCategoryQuery, Result<List<GetGymCategoryDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGymCategoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetGymCategoryDto>>> Handle(GetAllGymCategoryQuery request, CancellationToken cancellationToken)
    {
        var gyms = await _unitOfWork.Repository<GymCategory>().GetAll();

        var map = _mapper.Map<List<GetGymCategoryDto>>(gyms);

        return Result<List<GetGymCategoryDto>>.Success(map, "Gym List");
    }
}