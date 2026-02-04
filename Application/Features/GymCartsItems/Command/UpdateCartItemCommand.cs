
using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCartItem;
using Domain.Entities.GymProducts;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymCartsItems.Command;

public class UpdateCartItemCommand : IRequest<Result<CartItem>>
{
    public int Id { get; set; }

    public CreateCartItemCommand CreateCommand { get; set; } = new();

    public UpdateCartItemCommand(int id, CreateCartItemCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, Result<CartItem>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCartItemHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CartItem>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.GymProductId.HasValue)
        {
            var product = await _unitOfWork.Repository<CartItem>().GetByID(request.CreateCommand.GymProductId.Value);

            if (product == null)
            {
                return Result<CartItem>.BadRequest("Product id is not exist");
            }
        }
        var gym = await _unitOfWork.Repository<CartItem>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
        {
            if (gym == null)
            {
                return Result<CartItem>.BadRequest("soory id not found");
            }

            _mapper.Map(request.CreateCommand, gym);

            await _unitOfWork.Repository<CartItem>().UpdateAsync(gym);
            await _unitOfWork.Save(cancellationToken);

            return Result<CartItem>.Success("Updated CatItem");
        }
    }
}