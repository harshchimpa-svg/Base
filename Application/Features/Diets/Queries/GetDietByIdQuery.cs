using Application.Dto.Clientses;
using Application.Dto.Diets;
using Application.Features.Clients.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using Domain.Entities.Diets;
using MediatR;
using Shared;

namespace Application.Features.Diets.Queries;

public class GetDietByIdQuery: IRequest<Result<GetDietDto>>
{
    public int Id { get; set; }

    public GetDietByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetDietByIdQueryHandler : IRequestHandler<GetDietByIdQuery, Result<GetDietDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDietByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<GetDietDto>> Handle(GetDietByIdQuery request, CancellationToken cancellationToken)

    {
        var diets = await _unitOfWork.Repository<Diet>().GetByID(request.Id);

        if (diets == null)
        {
            return Result<GetDietDto>.BadRequest("Diets not found.");
        }

        var mapData = _mapper.Map<GetDietDto>(diets);

        return Result<GetDietDto>.Success(mapData, "Diets");
    }
}