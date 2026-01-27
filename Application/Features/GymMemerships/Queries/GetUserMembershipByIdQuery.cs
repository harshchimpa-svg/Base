

using Application.Dto.GymMemerships;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymMemerships;
using MediatR;
using Shared;

namespace Application.Features.GymMemerships.Queries;

public class GetUserMembershipByIdQuery : IRequest<Result<GetUserMembershipDto>>
{
    public int Id { get; set; }

    public GetUserMembershipByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetUserMembershipByIdQueryHandler
    : IRequestHandler<GetUserMembershipByIdQuery, Result<GetUserMembershipDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserMembershipByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetUserMembershipDto>> Handle(
        GetUserMembershipByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<UserMembership>()
            .GetByID(request.Id);

        if (entity == null)
        {
            return Result<GetUserMembershipDto>.BadRequest("User Membership not found");
        }

        var dto = _mapper.Map<GetUserMembershipDto>(entity);

        return Result<GetUserMembershipDto>.Success(dto, "UserMembership");
    }
}