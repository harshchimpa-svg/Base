
using Application.Dto.GymMemberships;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymMemberships;
using MediatR;
using Shared;

namespace Application.Features.GymMemberships.Queries;

public class GetAllUserMembershipQuery : IRequest<Result<List<GetUserMembershipDto>>>
{
}
internal class GetAllUserMembershipQueryHandler : IRequestHandler<GetAllUserMembershipQuery, Result<List<GetUserMembershipDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUserMembershipQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetUserMembershipDto>>> Handle(GetAllUserMembershipQuery request, CancellationToken cancellationToken)
    {
        var list = await _unitOfWork.Repository<UserMembership>()
            .GetAll();

        var map = _mapper.Map<List<GetUserMembershipDto>>(list);

        return Result<List<GetUserMembershipDto>>.Success(map, "User Membership List");
    }
}