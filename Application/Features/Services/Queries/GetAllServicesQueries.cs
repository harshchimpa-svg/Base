using Application.Dto.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using MediatR;
using Shared;

namespace Application.Features.Services.Queries;

public class GetAllServicesQueries: IRequest<Result<List<GetServiceDto>>>
{
}
internal class GetAllPaymentHeadQueryHandler : IRequestHandler<GetAllServicesQueries, Result<List<GetServiceDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentHeadQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<List<GetServiceDto>>> Handle(GetAllServicesQueries request, CancellationToken cancellationToken)  
    {
        var Service = await _unitOfWork.Repository<Service>().GetAll();

        var map = _mapper.Map<List<GetServiceDto>>(Service);

        return Result<List<GetServiceDto>>.Success(map, "Service list");
    }
}