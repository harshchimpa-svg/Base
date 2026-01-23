using Application.Dto.diteTypes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DiteTypes;
using MediatR;
using Shared;

namespace Application.Features.DiteTypes.Queries;

public class GetDiteTypeByIdQuery: IRequest<Result<GetDiteTypeDto>>
{
    public int Id { get; set; }

    public GetDiteTypeByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetDiteTypeByIdQueryHandler : IRequestHandler<GetDiteTypeByIdQuery, Result<GetDiteTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDiteTypeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<GetDiteTypeDto>> Handle(GetDiteTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<DiteType>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetDiteTypeDto>.BadRequest("DiteType not found.");
        }

        var mapData = _mapper.Map<GetDiteTypeDto>(location);

        return Result<GetDiteTypeDto>.Success(mapData, "DiteType");
    }
}