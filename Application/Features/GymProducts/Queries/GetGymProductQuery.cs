

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

public class GetGymProductQuery : IRequest<Result<List<GetGymProductDto>>>
{
}

internal class GetGymProductQueryHandler : IRequestHandler<GetGymProductQuery, Result<List<GetGymProductDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetGymProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetGymProductDto>>> Handle(GetGymProductQuery request, CancellationToken cancellationToken)
    {
        var gyms = await _unitOfWork.Repository<GymProduct>().GetAll();

        var map = _mapper.Map<List<GetGymProductDto>>(gyms);

        return Result<List<GetGymProductDto>>.Success(map, "Product List");
    }
}
