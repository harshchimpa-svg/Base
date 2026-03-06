using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Security.Claims;

namespace Application.Features.Customers.Commands;

public class ReminderCustomerCommand : IRequest<Result<string>>
{
}

internal class ReminderCustomerCommandHandler : IRequestHandler<ReminderCustomerCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReminderCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<string>> Handle(ReminderCustomerCommand request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
        {
            return Result<string>.BadRequest("User not authenticated");
        }

        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Result<string>.BadRequest("UserId not found in token");
        }

        var customers = await _unitOfWork
            .Repository<Customer>()
            .Entities
            .Where(x => x.UserId == userId
                        && x.Balance > 0
                        && x.IsActive
                        && !string.IsNullOrEmpty(x.Email))
            .ToListAsync(cancellationToken);

        if (!customers.Any())
        {
            return Result<string>.Success("No pending customers found");
        }

        foreach (var customer in customers)
        {
            var subject = "Payment Reminder - Pending Amount";

            var body = $@"
            Hello {customer.Name}
             
            This is a friendly reminder.
             
            Your Pending Amount is:
             
             ₹{customer.Balance}
             
            Please clear your dues as soon as possible.
            
             Thank you.
             ";
             
            await _emailService.SendEmail(customer.Email, subject, body);
        }

        return Result<string>.Success("Reminder emails sent successfully");
    }
}