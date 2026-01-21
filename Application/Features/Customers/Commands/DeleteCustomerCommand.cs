using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Customers.Commands;

public class DeleteCustomerCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteCustomerCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Customer>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Customer not found.");
        }

        await _unitOfWork.Repository<Customer>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Customer deleted successfully.");
    }
}