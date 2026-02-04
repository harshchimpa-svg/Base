using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Employees;
using MediatR;
using Shared;

namespace Application.Features.Employees.Commands;

public class CreateEmployeeCommand: IRequest<Result<string>>, ICreateMapFrom<Employee>
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }  
    public string Alterphonenumber  { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; } 
    public string City { get; set; }
    public string State { get; set; }
    public string country { get; set; }
    public string? RoleId { get; set; }
}

internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {   
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var Employee = _mapper.Map<Employee>(request);

        await _unitOfWork.Repository<Employee>().AddAsync(Employee);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Employee created successfully.");
    }
}

