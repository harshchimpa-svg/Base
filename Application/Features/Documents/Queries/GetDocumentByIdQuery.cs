using Application.Dto.Documents;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Documents;
using MediatR;
using Shared;

namespace Application.Features.Documents.Queries;

public class GetByIdDocumentQuery : IRequest<Result<GetDocumentDto>>
{
    public int Id { get; set; }

    public GetByIdDocumentQuery(int id)
    {
        Id = id;
    }
}

internal class GetByIdDocumentQueryHandler
    : IRequestHandler<GetByIdDocumentQuery, Result<GetDocumentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdDocumentQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetDocumentDto>> Handle(
        GetByIdDocumentQuery request,
        CancellationToken cancellationToken)
    {
        var document = await _unitOfWork.Repository<Document>()
            .GetByID(request.Id);

        if (document == null)
        {
            return Result<GetDocumentDto>.BadRequest("Document not found.");
        }

        var map = _mapper.Map<GetDocumentDto>(document);

        return Result<GetDocumentDto>.Success(map, "Document");
    }
}
