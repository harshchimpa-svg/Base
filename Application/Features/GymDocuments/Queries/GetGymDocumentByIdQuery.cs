

using Application.Dto.GymDocuments;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymDocuments;
using MediatR;
using Shared;

namespace Application.Features.GymDocuments.Queries;

public class GetGymDocumentByIdQuery : IRequest<Result<GetGymDocumentDto>>
{
    public int Id { get; set; }

    public GetGymDocumentByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetGymDocumentByIdQueryHandler : IRequestHandler<GetGymDocumentByIdQuery, Result<GetGymDocumentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public GetGymDocumentByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetGymDocumentDto>> Handle(GetGymDocumentByIdQuery request, CancellationToken cancellationToken)
    {
       var location = await _unitOfWork.Repository<GymDocument>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetGymDocumentDto>.BadRequest("Gym not found");
        }

        var mapdata = _mapper.Map<GetGymDocumentDto>(location);

        return Result<GetGymDocumentDto>.Success(mapdata, "GymDocument");
    }
}
