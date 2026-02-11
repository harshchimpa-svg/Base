using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Customers.Commands;

public class BlockCustomerCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}
internal class BlockCustomerCommandHandler : IRequestHandler<BlockCustomerCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlockCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(
        BlockCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer == null)
            return Result<string>.BadRequest("Customer not found");

        customer.IsActive = !customer.IsActive;

        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Customer blocked");
    }
}

