

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

public class GetByIdGymCategoryQuery : IRequest<Result<GetGymCategoryDto>>
{
    public int Id { get; set; }

    public GetByIdGymCategoryQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdGymCategoryQueryHandler : IRequestHandler<GetByIdGymCategoryQuery, Result<GetGymCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdGymCategoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetGymCategoryDto>> Handle(GetByIdGymCategoryQuery request, CancellationToken cancellationToken)
    {
        var gym = await _unitOfWork.Repository<GymCategory>().GetByID(request.Id);

        if (gym == null)
        {
            return Result<GetGymCategoryDto>.BadRequest("GYm Not Found");
        }

        var mapData = _mapper.Map<GetGymCategoryDto>(gym);

        return Result<GetGymCategoryDto>.Success(mapData, "Gym");
    }
}

