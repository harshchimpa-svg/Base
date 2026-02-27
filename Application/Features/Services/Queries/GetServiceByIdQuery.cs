using Application.Dto.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using MediatR;
using Shared;

namespace Application.Features.Services.Queries;

public class GetServiceByIdQuery: IRequest<Result<GetServiceDto>>
{
    public int Id { get; set; }

    public GetServiceByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetServicesByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Result<GetServiceDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetServicesByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetServiceDto>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken) 
    {
        var service = await _unitOfWork.Repository<Service>().GetByID(request.Id);

        if (service == null)
        {
            return Result<GetServiceDto>.BadRequest("Service not found.");
        }

        var mapData = _mapper.Map<GetServiceDto>(service);

        return Result<GetServiceDto>.Success(mapData, "Service");
    }
}