

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

public class GetAllProductDocumentQuery : IRequest<PaginatedResult<GetProductDocument>>
{
    public int? GymProductId { get; set; }
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
internal class GetAllProductDocumentQueryHandler : IRequestHandler<GetAllProductDocumentQuery, PaginatedResult<GetProductDocument>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductDocumentQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetProductDocument>> Handle(GetAllProductDocumentQuery request, CancellationToken cancellationToken)
    {
        var querable = _unitOfWork.Repository<ProductDocument>().Entities.AsQueryable();

        if (request.GymProductId.HasValue)
        {
            querable = querable.Where(x => x.GymProductId == request.GymProductId);
        }

        int count = await querable.CountAsync();

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            querable = querable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var query = await querable.ToListAsync();

        var map = _mapper.Map<List<GetProductDocument>>(query);

        return PaginatedResult<GetProductDocument>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
