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

public class GetEmployeeByIdQuery: IRequest<Result<GetEmployeeDTO>>
{
    public int Id { get; set; }

    public GetEmployeeByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetEmployeeByIdQueriesHandler : IRequestHandler<GetEmployeeByIdQuery, Result<GetEmployeeDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetEmployeeDTO>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByID(request.Id);

        if (employee == null)
        {
            return Result<GetEmployeeDTO>.BadRequest("Employee not found.");
        }

        var mapData = _mapper.Map<GetEmployeeDTO>(employee);

        return Result<GetEmployeeDTO>.Success(mapData, "Employee");
    }
}