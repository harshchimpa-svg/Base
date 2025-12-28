using Application.Dto.PaymentHeades;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.PaymentHeates;
using MediatR;
using Shared;

namespace Application.Features.PaymentHeades.Queries;

public class GetPaymentHeadByIdQuery : IRequest<Result<GetPaymentHeadDto>>
{
    public int Id { get; set; }

    public GetPaymentHeadByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetPaymentHeadByIdQueryHandler : IRequestHandler<GetPaymentHeadByIdQuery, Result<GetPaymentHeadDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPaymentHeadByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetPaymentHeadDto>> Handle(GetPaymentHeadByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<PaymentHead>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetPaymentHeadDto>.BadRequest("Location not found.");
        }

        var mapData = _mapper.Map<GetPaymentHeadDto>(location);

        return Result<GetPaymentHeadDto>.Success(mapData, "Location");
    }
}