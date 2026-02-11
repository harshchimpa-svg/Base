using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Customers.Commands;

public class UpdateCustomerCommand : IRequest<Result<Customer>>
{
    public int Id { get; set; }
    public CreateCustomerCommand CreateCommand { get; set; } = new();

    public UpdateCustomerCommand(int id, CreateCustomerCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
public record GetCustomerDto(IFormFile File);

internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<Customer>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Result<Customer>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer == null)
            return Result<Customer>.BadRequest("Customer not found");

        if (request.CreateCommand.Profile != null)
        {
            customer.Profile = await _fileService.UploadAsync(request.CreateCommand.Profile, "Customer");
        }

        customer.Name = request.CreateCommand.Name;
        customer.Email = request.CreateCommand.Email;
        customer.PhoneNumber = request.CreateCommand.PhoneNumber;
        customer.Notes = request.CreateCommand.Notes;

        await _unitOfWork.Repository<Customer>().UpdateAsync(customer);
        await _unitOfWork.Save(cancellationToken);

        return Result<Customer>.Success(customer, "Customer updated successfully");
    }
}
