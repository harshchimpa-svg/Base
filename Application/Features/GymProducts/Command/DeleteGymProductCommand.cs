
using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymProducts;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymProducts.Command;

public class DeleteGymProductCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteGymProductCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteGymProductCommandHandler : IRequestHandler<DeleteGymProductCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteGymProductCommand request, CancellationToken cancellationToken)
    {
        var gymExists = await _unitOfWork.Repository<GymProduct>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!gymExists)
        {
            return Result<bool>.BadRequest("Product not Found");
        }

        await _unitOfWork.Repository<GymProduct>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Product deleted succesfully");
    }
}
