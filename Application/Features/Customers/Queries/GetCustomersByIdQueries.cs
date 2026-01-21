using Application.Dto.Customers;
using Application.Dto.Services;
using Application.Features.Services.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using MediatR;
using Shared;

namespace Application.Features.Customers.Queries;

public class GetCustomersByIdQueries: IRequest<Result<GetCustomerDto>>
{
    public int Id { get; set; }

    public GetCustomersByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetCustomersByIdQueriesHandler : IRequestHandler<GetCustomersByIdQueries, Result<GetCustomerDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomersByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCustomerDto>> Handle(GetCustomersByIdQueries request, CancellationToken cancellationToken)
    {
        var Service = await _unitOfWork.Repository<Service>().GetByID(request.Id);

        if (Service == null)
        {
            return Result<GetCustomerDto>.BadRequest("Service not found.");
        }

        var mapData = _mapper.Map<GetCustomerDto>(Service);

        return Result<GetCustomerDto>.Success(mapData, "Service");
    }
}