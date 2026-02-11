using Application.Dto.PaymentLoges;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.PaymentLoges;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.PaymentLoges.Queries;

public class GetAllPaymentLogeQueries : IRequest<PaginatedResult<GetPaymentLogeDto>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

internal class GetAllPaymentLogeQueriesHandler
    : IRequestHandler<GetAllPaymentLogeQueries, PaginatedResult<GetPaymentLogeDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentLogeQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetPaymentLogeDto>> Handle(GetAllPaymentLogeQueries request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork
            .Repository<PaymentLoge>()
            .Entities
            .Include(x => x.User)
            .Include(x => x.Customer)
            .Include(x => x.Transaction)
            .AsQueryable();

        if (request.FromDate.HasValue)
        {
            queryable = queryable.Where(x => x.CreatedDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            queryable = queryable.Where(x => x.CreatedDate <= request.ToDate.Value);
        }

        int count = await queryable.CountAsync(cancellationToken);

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .OrderByDescending(x => x.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        var query = await queryable.AsNoTracking().ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetPaymentLogeDto>>(query);

        return PaginatedResult<GetPaymentLogeDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
