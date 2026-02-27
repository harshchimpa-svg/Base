using Application.Common.Mappings.Commons;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared;
using System.Security.Claims;

namespace Application.Features.Customers.Command;

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
    private readonly UserManager<User> _userManager;
    private readonly IFileService _fileService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateCustomerCommandHandler(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IFileService fileService,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _fileService = fileService;
        _httpContextAccessor = httpContextAccessor; 
    }

    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
        {
            return Result<string>.BadRequest("user note exist");
        }

        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Result<string>.BadRequest("UserId not found ");
        }

        string profileUrl = null;

        if (request.Profile != null)
        {
            profileUrl = await _fileService.UploadAsync(request.Profile, "Customer");
        }

        var customer = new Customer
        {
            UserId = userId, 
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
