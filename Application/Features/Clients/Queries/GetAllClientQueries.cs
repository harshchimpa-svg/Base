using Application.Dto.Clientses;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Clientses.Queries;

public class GetAllClientQueries : IRequest<PaginatedResult<GetClientsDto>>
{
    public int? ServiceId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllClientsQueriesHandler : IRequestHandler<GetAllClientQueries,PaginatedResult<GetClientsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllClientsQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetClientsDto>> Handle(GetAllClientQueries request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Clients>().Entities.Include(s => s.Service)
            .AsQueryable();

        if (request.ServiceId.HasValue)
        {
            queryable = queryable.Where(x => x.ServiceId == request.ServiceId);
        }
        int count = await queryable.CountAsync();


        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetClientsDto>>(query);

        return PaginatedResult <GetClientsDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}