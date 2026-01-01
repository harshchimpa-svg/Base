using Application.Dto.Transicstions;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Domain.Entities.TranstionDocuments;
using MediatR;
using Shared;

namespace Application.Features.TranstionDocuments.Queries;

public class GetAllTranstionDocumentQuery : IRequest<Result<List<GetTransicstionDocumentsDto>>>
{
    public int? TransicstionId { get; set; }
    public int? CatgoryId { get; set; } 

}
internal class GetAllTranstionDocumentQueryHandler : IRequestHandler<GetAllTranstionDocumentQuery, Result<List<GetTransicstionDocumentsDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTranstionDocumentQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetTransicstionDocumentsDto>>> Handle(GetAllTranstionDocumentQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<TranstionDocument>().GetAll();

        var map = _mapper.Map<List<GetTransicstionDocumentsDto>>(locations);

        return Result<List<GetTransicstionDocumentsDto>>.Success(map, "Location list");
    }
}