using Application.Dto.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using MediatR;
using Shared;

namespace Application.Features.Services.Queries;

public class GetAllServiceQuery: IRequest<Result<List<GetServiceDto>>>
{
}
internal class GetAllPaymentHeadQueryHandler : IRequestHandler<GetAllServiceQuery, Result<List<GetServiceDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentHeadQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<List<GetServiceDto>>> Handle(GetAllServiceQuery request, CancellationToken cancellationToken)  
    {
        var service = await _unitOfWork.Repository<Service>().GetAll();

        var map = _mapper.Map<List<GetServiceDto>>(service);

        return Result<List<GetServiceDto>>.Success(map, "Service list");
    }
}