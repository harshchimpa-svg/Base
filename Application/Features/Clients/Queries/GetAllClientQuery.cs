using Application.Dto.Clientses;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Clients.Queries;

public class GetAllClientQuery : IRequest<PaginatedResult<GetClientDto>>
{
    public int? ServiceId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery,PaginatedResult<GetClientDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllClientQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetClientDto>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Client>().Entities.Include(s => s.Service)
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

        var map = _mapper.Map<List<GetClientDto>>(query);

        return PaginatedResult <GetClientDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}