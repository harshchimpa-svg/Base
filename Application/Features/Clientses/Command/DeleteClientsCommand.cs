using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Clientses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Clientses.Command;

public class DeleteClientsCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteClientsCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteClientsCommandHandler : IRequestHandler<DeleteClientsCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteClientsCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Clients>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Clients not found.");
        }

        await _unitOfWork.Repository<Clients>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Clients deleted successfully.");
    }
}