

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
       var gymDocuments = await _unitOfWork.Repository<GymDocument>().GetByID(request.Id);

        if (gymDocuments == null)
        {
            return Result<GetGymDocumentDto>.BadRequest("GymDocuments not found");
        }

        var mapData = _mapper.Map<GetGymDocumentDto>(gymDocuments);                            

        return Result<GetGymDocumentDto>.Success(mapData, "GymDocument");
    }
}
