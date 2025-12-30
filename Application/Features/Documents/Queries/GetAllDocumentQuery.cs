using Application.Dto.Documents;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Documents;
using MediatR;
using Shared;

namespace Application.Features.Documents.Queries;

public class GetDocumentQuery : IRequest<Result<List<GetDocumentDto>>>
{
}

internal class GetDocumentQueryHandler
    : IRequestHandler<GetDocumentQuery, Result<List<GetDocumentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDocumentQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetDocumentDto>>> Handle(
        GetDocumentQuery request,
        CancellationToken cancellationToken)
    {
        var documents = await _unitOfWork.Repository<Document>().GetAll();

        var map = _mapper.Map<List<GetDocumentDto>>(documents);

        return Result<List<GetDocumentDto>>.Success(map, "Document list");
    }
}
