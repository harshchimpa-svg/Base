using Application.Dto.Balences;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Balances;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Queries;

public class GetAllBalenceQuery : IRequest<PaginatedResult<GetBalenceDto>>
{
    public int? CustomerId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllBalenceQueryHandler : IRequestHandler<GetAllBalenceQuery,PaginatedResult<GetBalenceDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBalenceQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetBalenceDto>> Handle(GetAllBalenceQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Balance>().Entities.Include(s => s.Customer)
            .AsQueryable();

        if (request.CustomerId.HasValue)
        {
            queryable = queryable.Where(x => x.CustomerId == request.CustomerId);
        }
        int count = await queryable.CountAsync();


        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetBalenceDto>>(query);

        return PaginatedResult < GetBalenceDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}