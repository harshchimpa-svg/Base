
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymMemerships;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymMemerships;

public class UpdateUserMembershipCommand : IRequest<Result<UserMembership>>
{
    public int Id { get; set; }
    public CreateUserMembershipCommand CreateCommand { get; set; }

    public UpdateUserMembershipCommand( 

        int id,
        CreateUserMembershipCommand command)
    {
        Id = id;
        CreateCommand = command;
    }
}
internal class UpdateUserMembershipCommandHandler
    : IRequestHandler<UpdateUserMembershipCommand, Result<UserMembership>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserMembershipCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserMembership>> Handle(
        UpdateUserMembershipCommand request,
        CancellationToken cancellationToken)
    {
        var membership = await _unitOfWork.Repository<UserMembership>()
            .Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (membership == null)
        {
            return Result<UserMembership>.BadRequest("User Membership not found");
        }

        if (request.CreateCommand.EndDate <= request.CreateCommand.StartDate)
        {
            return Result<UserMembership>.BadRequest("Invalid date range");
        }

        _mapper.Map(request.CreateCommand, membership);

        await _unitOfWork.Repository<UserMembership>()
            .UpdateAsync(membership);

        await _unitOfWork.Save(cancellationToken);

        return Result<UserMembership>.Success(membership, "User Membership Updated");
    }
}
