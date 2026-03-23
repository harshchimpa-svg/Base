/*using Application.Features.Customers.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Customers;
using Domain.Entities.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Employees.Commands;

public class DeleteEmployeeCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteEmployeeCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
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
}*/