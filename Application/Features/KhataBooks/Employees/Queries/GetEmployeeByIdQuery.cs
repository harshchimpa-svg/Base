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

public class GetEmployeeByIdQuery: IRequest<Result<GetEmployeeDto>>
{
    public int Id { get; set; }

    public GetEmployeeByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<GetEmployeeDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetEmployeeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetEmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByID(request.Id);

        if (employee == null)
        {
            return Result<GetEmployeeDto>.BadRequest("Employee not found.");
        }

        var mapData = _mapper.Map<GetEmployeeDto>(employee);

        return Result<GetEmployeeDto>.Success(mapData, "Employee");
    }
}