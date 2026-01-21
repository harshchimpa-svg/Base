using Application.Dto.Balences;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Balances;
using MediatR;
using Shared;

namespace Application.Features.Balence.Queries;

public class GetAllBalenceQuery: IRequest<Result<List<GetBalenceDto>>>
{
}
internal class GetAllCatgoryesQueryHandler : IRequestHandler<GetAllBalenceQuery, Result<List<GetBalenceDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCatgoryesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<GetBalenceDto>>> Handle(GetAllBalenceQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Balance>().GetAll();

        var map = _mapper.Map<List<GetBalenceDto>>(locations);

        return Result<List<GetBalenceDto>>.Success(map, "Location list");
    }
}