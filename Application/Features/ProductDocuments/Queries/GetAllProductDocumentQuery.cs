

using Application.Dto.GymProducts;
using Application.Dto.ProductDocuments;
using Application.Features.GymDocuments.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymDocuments;
using Domain.Entities.ProductDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ProductDocuments.Queries;

public class GetAllProductDocumentQuery : IRequest<PaginatedResult<GetProductDocumentDto>>
{
    public int? GymProductId { get; set; }
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
internal class GetAllProductDocumentQueryHandler : IRequestHandler<GetAllProductDocumentQuery, PaginatedResult<GetProductDocumentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductDocumentQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetProductDocumentDto>> Handle(GetAllProductDocumentQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<ProductDocument>().Entities.AsQueryable();

        if (request.GymProductId.HasValue)
        {
            queryable = queryable.Where(x => x.GymProductId == request.GymProductId);
        }

        int count = await queryable.CountAsync();

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetProductDocumentDto>>(query);

        return PaginatedResult<GetProductDocumentDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
