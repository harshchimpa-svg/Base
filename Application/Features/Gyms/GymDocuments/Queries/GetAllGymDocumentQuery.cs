
using Application.Dto.GymDocuments;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymDocuments.Queries;

public class GetAllGymDocumentQuery : IRequest<PaginatedResult<GetGymDocumentDto>>
{
    public int? GymId { get; set; }
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
internal class GetAllGymDocumentQueryHandler : IRequestHandler<GetAllGymDocumentQuery, PaginatedResult<GetGymDocumentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGymDocumentQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetGymDocumentDto>> Handle(GetAllGymDocumentQuery request, CancellationToken cancellationToken)
    {
        var  querable = _unitOfWork.Repository<GymDocument>().Entities.AsQueryable();

        if (request.GymId.HasValue)
        {
            querable = querable.Where(x => x.GymId == request.GymId);
        }

        int count = await querable.CountAsync();

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            querable = querable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var query = await querable.ToListAsync();

        var map = _mapper.Map<List<GetGymDocumentDto>>(query);

        return PaginatedResult<GetGymDocumentDto>.Create(map,count,request.PageNumber, request.PageSize);
    }
}
