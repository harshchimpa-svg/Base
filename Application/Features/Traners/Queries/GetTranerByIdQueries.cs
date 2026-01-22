using Application.Dto.Users.GetUserDtos;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Application.Features.Tranners.Queries;

public class GetTranerByIdQuery : IRequest<Result<GetUserDto>>
{
    public int Id { get; set; }

    public GetTranerByIdQuery(int id)
    {
        Id = id;
    }
}

internal class GetTrannerByIdQueryHandler : IRequestHandler<GetTranerByIdQuery, Result<GetUserDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public GetTrannerByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork,UserManager<User> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetUserDto>> Handle(GetTranerByIdQuery request, CancellationToken cancellationToken)
    {
        var tranner = await _unitOfWork.Repository<User>().GetByID(request.Id);

        if (tranner == null)
            return Result<GetUserDto>.BadRequest("Tranner not found");

        var map = _mapper.Map<GetUserDto>(tranner);

        return Result<GetUserDto>.Success(map, "Tranner detail");
    }
}