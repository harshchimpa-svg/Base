

using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.GymMemerships;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymMemerships;

public class DeleteUserMembershipCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public DeleteUserMembershipCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteUserMembershipCommandHandler
    : IRequestHandler<DeleteUserMembershipCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserMembershipCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteUserMembershipCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.Repository<UserMembership>()
            .Entities.AnyAsync(x => x.Id == request.Id);

        if (!exists)
        {
            return Result<bool>.BadRequest("User Membership not found");
        }

        await _unitOfWork.Repository<UserMembership>()
            .DeleteAsync(request.Id);

        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "User Membership deleted successfully");
    }
}
