using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Clientses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Clients.Command;

public class DeleteClientCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteClientCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Client>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Clients not found.");
        }

        await _unitOfWork.Repository<Client>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Clients deleted successfully.");
    }
}