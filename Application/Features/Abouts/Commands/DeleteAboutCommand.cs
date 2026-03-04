using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Abouts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Abouts.Command;

public class DeleteAboutCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteAboutCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteAboutCommandHandler : IRequestHandler<DeleteAboutCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAboutCommandHandler(IUnitOfWork unitOfWork) 
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteAboutCommand request, CancellationToken cancellationToken)
    {
        var abouteExists = await _unitOfWork.Repository<About>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!abouteExists)
        {
            return Result<bool>.BadRequest("About not found.");
        }

        await _unitOfWork.Repository<About>().DeleteAsync(request.Id); 
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "About deleted successfully.");
    }
}