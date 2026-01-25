using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Services.Command;

public class DeleteServiceCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteServiceCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteServicesCommandHandler : IRequestHandler<DeleteServiceCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServicesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Service>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Service not found.");
        }

        await _unitOfWork.Repository<Service>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Service deleted successfully.");
    }
}