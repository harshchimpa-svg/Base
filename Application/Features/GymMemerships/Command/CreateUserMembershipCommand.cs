
using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.GymMemerships;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymMemerships;

public class CreateUserMembershipCommand : IRequest<Result<string>>,ICreateMapFrom<UserMembership>
{
    public string UserId { get; set; }
    public int MembershipId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
internal class CreateUserMembershipCommandHandler
    : IRequestHandler<CreateUserMembershipCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CreateUserMembershipCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(
        CreateUserMembershipCommand request,
        CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
        {
            return Result<string>.BadRequest("User id not exist");
        }

        if (request.EndDate <= request.StartDate)
        {
            return Result<string>.BadRequest("Invalid date range");
        }

        var membership = new UserMembership
        {
            UserId = request.UserId,
            MembershipId = request.MembershipId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        await _unitOfWork.Repository<UserMembership>()
            .AddAsync(membership);

        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("User Membership Created Successfully");
    }
}