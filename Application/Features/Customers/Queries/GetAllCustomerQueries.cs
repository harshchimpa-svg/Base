using Application.Dto.Customers;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using MediatR;
using Shared;

namespace Application.Features.Customers.Queries;

public class GetAllCustomerQueries: IRequest<Result<List<GetCustomerDto>>>
{
}
internal class GetAllCustomersQueriesHandler : IRequestHandler<GetAllCustomerQueries, Result<List<GetCustomerDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCustomersQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetCustomerDto>>> Handle(GetAllCustomerQueries request, CancellationToken cancellationToken)
    {
        var Service = await _unitOfWork.Repository<Customer>().GetAll();

        var map = _mapper.Map<List<GetCustomerDto>>(Service);

        return Result<List<GetCustomerDto>>.Success(map, "Service list");
    }
}