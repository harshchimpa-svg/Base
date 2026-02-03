using Application.Dto.PaymentLoges;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.PaymentLoges;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.PaymentLoges.Queries;

public class GetAllPaymentLogeQueries : IRequest<Result<List<GetPaymentLogeDto>>>
{
}

internal class GetAllPaymentLogeQueriesHandler : IRequestHandler<GetAllPaymentLogeQueries, Result<List<GetPaymentLogeDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentLogeQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetPaymentLogeDto>>> Handle(GetAllPaymentLogeQueries request, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork
            .Repository<PaymentLoge>()
            .Entities
            .Include(x => x.User)
            .Include(x => x.Customer)
            .Include(x => x.Transaction)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var map = _mapper.Map<List<GetPaymentLogeDto>>(logs);

        return Result<List<GetPaymentLogeDto>>.Success(map, "PaymentLoge list");
    }
}