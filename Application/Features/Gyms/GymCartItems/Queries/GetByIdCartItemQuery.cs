
using Application.Dto.GymCartItems;
using Application.Dto.Gyms;
using Application.Features.Gyms.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCartItem;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.GymCartItems.Queries;

public class GetByIdCartItemQuery : IRequest<Result<GetCartItemDto>>
{
    public int Id { get; set; }

    public GetByIdCartItemQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdCartItemQueryHandler : IRequestHandler<GetByIdCartItemQuery, Result<GetCartItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdCartItemQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCartItemDto>> Handle(GetByIdCartItemQuery request, CancellationToken cancellationToken)
    {
        var gymCartItems = await _unitOfWork.Repository<CartItem>().GetByID(request.Id);

        if (gymCartItems == null)
        {
            return Result<GetCartItemDto>.BadRequest("GymCartItems Not Found");
        }

        var mapData = _mapper.Map<GetCartItemDto>(gymCartItems);

        return Result<GetCartItemDto>.Success(mapData, "GymCartItems");
    }
}