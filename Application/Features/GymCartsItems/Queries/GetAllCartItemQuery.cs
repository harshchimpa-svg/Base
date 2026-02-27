
using Application.Dto.GymCartItems;
using Application.Dto.Gyms;
using Application.Features.Gyms.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCartItem;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.GymCartsItems.Queries;

public class GetAllCartItemQuery : IRequest<Result<List<GetCartItemDto>>>
{

}
internal class GetAllCartItemQueryHandler : IRequestHandler<GetAllCartItemQuery, Result<List<GetCartItemDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCartItemQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetCartItemDto>>> Handle(GetAllCartItemQuery request, CancellationToken cancellationToken)
    {
        var gyms = await _unitOfWork.Repository<CartItem>().GetAll();

        var map = _mapper.Map<List<GetCartItemDto>>(gyms);

        return Result<List<GetCartItemDto>>.Success(map, "CartItem List");
    }
}