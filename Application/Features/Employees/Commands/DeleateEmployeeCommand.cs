using Application.Features.Customers.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Customers;
using Domain.Entities.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Employees.Commands;

public class DeleateEmployeeCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateEmployeeCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateEmployeeCommandHandler : IRequestHandler<DeleateEmployeeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Employee>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Customer not found.");
        }

        await _unitOfWork.Repository<Employee>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Customer deleted successfully.");
    }
}