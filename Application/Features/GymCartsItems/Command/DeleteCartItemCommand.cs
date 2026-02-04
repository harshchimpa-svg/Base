
using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymCartItem;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymCartsItems.Command;

public class DeleteCartItemCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteCartItemCommand(int id)
    {
        Id = id;
    }

}
internal class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCartItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var gymExists = await _unitOfWork.Repository<CartItem>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!gymExists)
        {
            return Result<bool>.BadRequest("CartItem not Found");
        }

        await _unitOfWork.Repository<CartItem>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "CartItem deleted succesfully");
    }
}
