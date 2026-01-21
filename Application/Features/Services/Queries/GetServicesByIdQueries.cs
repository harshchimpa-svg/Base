using Application.Dto.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using MediatR;
using Shared;

namespace Application.Features.Services.Queries;

public class GetServicesByIdQueries: IRequest<Result<GetServiceDto>>
{
    public int Id { get; set; }

    public GetServicesByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetServicesByIdQueriesHandler : IRequestHandler<GetServicesByIdQueries, Result<GetServiceDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetServicesByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetServiceDto>> Handle(GetServicesByIdQueries request, CancellationToken cancellationToken) 
    {
        var Service = await _unitOfWork.Repository<Service>().GetByID(request.Id);

        if (Service == null)
        {
            return Result<GetServiceDto>.BadRequest("Service not found.");
        }

        var mapData = _mapper.Map<GetServiceDto>(Service);

        return Result<GetServiceDto>.Success(mapData, "Service");
    }
}