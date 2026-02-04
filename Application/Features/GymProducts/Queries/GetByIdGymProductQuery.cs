
using Application.Dto.GymProducts;
using Application.Dto.Gyms;
using Application.Features.Gyms.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymProducts;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.GymProducts.Queries;

public class GetByIdGymProductQuery : IRequest<Result<GetGymProductDto>>
{
    public int Id { get; set; }

    public GetByIdGymProductQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdGymProductQueryHandler : IRequestHandler<GetByIdGymProductQuery, Result<GetGymProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdGymProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetGymProductDto>> Handle(GetByIdGymProductQuery request, CancellationToken cancellationToken)
    {
        var gym = await _unitOfWork.Repository<GymProduct>().GetByID(request.Id);

        if (gym == null)
        {
            return Result<GetGymProductDto>.BadRequest("Product Not Found");
        }

        var mapData = _mapper.Map<GetGymProductDto>(gym);

        return Result<GetGymProductDto>.Success(mapData, "Gym");
    }
}
