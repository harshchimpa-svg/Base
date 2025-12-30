using Application.Dto.Transicstions;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.TranstionDocuments;
using MediatR;
using Shared;

namespace Application.Features.TranstionDocuments.Queries;

public class GetAllTranstionDocumentQuery : IRequest<PaginatedResult<GetTransicstionDocumentsDto>>
{
    public int? TransicstionId { get; set; }
    public int? CatgoryId { get; set; } 

}
internal class GetAllTranstionDocumentQueryHandler : IRequestHandler<GetAllTranstionDocumentQuery, PaginatedResult<GetTransicstionDocumentsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTranstionDocumentQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetTransicstionDocumentsDto>> Handle(GetAllTranstionDocumentQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<TranstionDocument>().Entities.AsQueryable();

        if (request.CatgoryId.HasValue)
        {
            queryable = queryable.Where(x => x.CatgoryId == request.CatgoryId);
        }

        var map = _mapper.Map<List<GetTransicstionDocumentsDto>>(queryable);

        return PaginatedResult < GetTransicstionDocumentsDto>.Create(map, 1, 1, 10);
    }
}