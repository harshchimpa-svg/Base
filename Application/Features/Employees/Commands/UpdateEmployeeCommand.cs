using Application.Features.Customers.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ApplicationRoles;
using Domain.Entities.Customers;
using Domain.Entities.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Employees.Commands;

public class UpdateEmployeeCommand: IRequest<Result<Employee>>
{

    public int Id { get; set; }
    public CreateEmployeeCommand CreateCommand { get; set; } = new();

    public UpdateEmployeeCommand(int id, CreateEmployeeCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<Employee>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Employee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var Service = await _unitOfWork.Repository<Employee>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Service == null)
        {
            return Result<Employee>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Service);

        await _unitOfWork.Repository<Employee>().UpdateAsync(Service);
        await _unitOfWork.Save(cancellationToken);

        return Result<Employee>.Success("Update Employee...");
    }
}