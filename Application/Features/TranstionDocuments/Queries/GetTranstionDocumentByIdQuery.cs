using Application.Dto.Transicstions;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.TranstionDocuments;
using MediatR;
using Shared;

namespace Application.Features.TranstionDocuments.Queriesl;

public class GetTranstionDocumentByIdQuery : IRequest<Result<GetTransicstionDocumentsDto>>
{
    public int Id { get; set; }

    public GetTranstionDocumentByIdQuery(int id)
    {
        Id = id;
    } 
}
internal class GetTranstionDocumentByIdQueryHandler : IRequestHandler<GetTranstionDocumentByIdQuery, Result<GetTransicstionDocumentsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTranstionDocumentByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetTransicstionDocumentsDto>> Handle(GetTranstionDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<TranstionDocument>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetTransicstionDocumentsDto>.BadRequest("Location not found.");
        }

        var mapData = _mapper.Map<GetTransicstionDocumentsDto>(location);

        return Result<GetTransicstionDocumentsDto>.Success(mapData, "Location");
    }
}