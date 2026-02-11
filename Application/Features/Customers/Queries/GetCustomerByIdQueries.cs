using Application.Dto.Customers;

using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.TransactionTypes;
using Domain.Entities.Customers;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Customers.Queries;

public class GetCustomerByIdQueries: IRequest<Result<GetCustomerDto>>
{
    public int Id { get; set; }

    public GetCustomerByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetCustomersByIdQueriesHandler : IRequestHandler<GetCustomerByIdQueries, Result<GetCustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomersByIdQueriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCustomerDto>> Handle(GetCustomerByIdQueries request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (customer == null)
            return Result<GetCustomerDto>.BadRequest("Customer not found");

        var totalCredit = await _unitOfWork.Repository<Transaction>()
            .Entities
            .Where(x => x.CustomerId == customer.Id
                        && x.TransactionType == TransactionType.Credit)
            .SumAsync(x => x.Amount);

        var totalDebit = await _unitOfWork.Repository<Transaction>()
            .Entities
            .Where(x => x.CustomerId == customer.Id
                        && x.TransactionType == TransactionType.Debit)
            .SumAsync(x => x.Amount);

        var balance = totalCredit - totalDebit;

        return Result<GetCustomerDto>.Success(new GetCustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Notes = customer.Notes,
            Balance = balance
        }, "Customer with balance");
    }
}
