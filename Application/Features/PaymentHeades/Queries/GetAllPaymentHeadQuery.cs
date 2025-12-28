using Application.Dto.PaymentHeades;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.PaymentHeates;
using MediatR;
using Shared;

namespace Application.Features.PaymentHeades.Queries;

public class GetAllPaymentHeadQuery : IRequest<Result<List<GetPaymentHeadDto>>>
{
}
internal class GetAllPaymentHeadQueryHandler : IRequestHandler<GetAllPaymentHeadQuery, Result<List<GetPaymentHeadDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentHeadQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetPaymentHeadDto>>> Handle(GetAllPaymentHeadQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<PaymentHead>().GetAll();

        var map = _mapper.Map<List<GetPaymentHeadDto>>(locations);

        return Result<List<GetPaymentHeadDto>>.Success(map, "Location list");
    }
}