/*using Application.Features.Customers.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Customers;
using Domain.Entities.GymTraners;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymTraners.Commands;

public class DeleteGymTranerCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteGymTranerCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteGymTranerCommandHandler : IRequestHandler<DeleteGymTranerCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymTranerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteGymTranerCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<GemTraner>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("GemTraner not found.");
        }

        await _unitOfWork.Repository<GemTraner>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "GemTraner deleted successfully.");
    }
}*/