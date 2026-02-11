using Application.Common.Mappings.Commons;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Customers.Commands;

public class CreateCustomerCommand : IRequest<Result<string>>, ICreateMapFrom<Customer>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Notes { get; set; }
    public IFormFile? Profile { get; set; }
    public decimal? Balance { get; set; }
}

internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }
    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        string profileUrl = null;

        if (request.Profile != null)
        {
            profileUrl = await _fileService.UploadAsync(request.Profile, "Customer");
        }

        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Notes = request.Notes,
            Balance = request.Balance,
            Profile = profileUrl,
            IsActive = true
        };

        await _unitOfWork.Repository<Customer>().AddAsync(customer);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Customer created successfully");
    }
}
