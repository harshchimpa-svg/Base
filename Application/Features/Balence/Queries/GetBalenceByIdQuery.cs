using Application.Dto.Balences;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Balances;
using MediatR;
using Shared;

namespace Application.Features.Balence.Queries;

public class GetBalenceByIdQuery: IRequest<Result<GetBalenceDto>>
{
    public int Id { get; set; }

    public GetBalenceByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetBalenceByIdQueryHandler : IRequestHandler<GetBalenceByIdQuery, Result<GetBalenceDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetBalenceByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<GetBalenceDto>> Handle(GetBalenceByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Balance>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetBalenceDto>.BadRequest("Location not found.");
        }

        var mapData = _mapper.Map<GetBalenceDto>(location);

        return Result<GetBalenceDto>.Success(mapData, "Location");
    }
}