using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Abouts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Abouts.Commands;

public class DeleateAboutCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateAboutCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateAboutCommandHandler : IRequestHandler<DeleateAboutCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateAboutCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateAboutCommand request, CancellationToken cancellationToken)
    {
        var CategoryExists = await _unitOfWork.Repository<About>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!CategoryExists)
        {
            return Result<bool>.BadRequest("About not found.");
        }

        await _unitOfWork.Repository<About>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "About deleted successfully.");
    }
}