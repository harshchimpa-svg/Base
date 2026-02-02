using Application.Dto.Customers;
using Application.Dto.Employees;
using Application.Features.Customers.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using Domain.Entities.Employees;
using MediatR;
using Shared;

namespace Application.Features.Employees.Queries;

public class GetEmployeeByIdQueries: IRequest<Result<GetEmployeeDTO>>
{
    public int Id { get; set; }

    public GetEmployeeByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetEmployeeByIdQueriesHandler : IRequestHandler<GetEmployeeByIdQueries, Result<GetEmployeeDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetEmployeeDTO>> Handle(GetEmployeeByIdQueries request, CancellationToken cancellationToken)
    {
        var Employee = await _unitOfWork.Repository<Employee>().GetByID(request.Id);

        if (Employee == null)
        {
            return Result<GetEmployeeDTO>.BadRequest("Employee not found.");
        }

        var mapData = _mapper.Map<GetEmployeeDTO>(Employee);

        return Result<GetEmployeeDTO>.Success(mapData, "Employee");
    }
}