using Application.Dto.Customers;

using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using MediatR;
using Shared;

namespace Application.Features.Customers.Queries;

public class GetCustomerByIdQueries: IRequest<Result<GetCustomerDto>>
{
    public int Id { get; set; }

    public GetCustomerByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetCustomersByIdQueriesHandler : IRequestHandler<GetCustomerByIdQueries, Result<GetCustomerDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomersByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCustomerDto>> Handle(GetCustomerByIdQueries request, CancellationToken cancellationToken)
    {
        var Service = await _unitOfWork.Repository<Customer>().GetByID(request.Id);

        if (Service == null)
        {
            return Result<GetCustomerDto>.BadRequest("Service not found.");
        }

        var mapData = _mapper.Map<GetCustomerDto>(Service);

        return Result<GetCustomerDto>.Success(mapData, "Service");
    }
}