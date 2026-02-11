
using Application.Dto.GymMemerships;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymMemerships;
using MediatR;
using Shared;

namespace Application.Features.GymMemerships.Queries;

public class GetUserMembershipQuery : IRequest<Result<List<GetUserMembershipDto>>>
{
}
internal class GetUserMembershipQueryHandler : IRequestHandler<GetUserMembershipQuery, Result<List<GetUserMembershipDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserMembershipQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetUserMembershipDto>>> Handle(GetUserMembershipQuery request, CancellationToken cancellationToken)
    {
        var list = await _unitOfWork.Repository<UserMembership>()
            .GetAll();

        var map = _mapper.Map<List<GetUserMembershipDto>>(list);

        return Result<List<GetUserMembershipDto>>
            .Success(map, "User Membership List");
    }
}