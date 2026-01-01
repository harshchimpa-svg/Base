using Application.Common.Mappings.Commons;
using Application.Interfaces.Repositories.Otps;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.Otps;
using Domain.Common.Enums.Users;
using Domain.Entities.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Commands;

public class UserRegistrationCommand : IRequest<Result<string>>, ICreateMapFrom<User>
{
    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters")]
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [Length(6, 30, ErrorMessage = "Password must between 6 to 30 characters")]
    public string Password { get; set; }
}

internal class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserIdAndOrganizationIdRepository _userIdAndOrganizationIdRepository;
    private readonly IOtpRepository _otpRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public UserRegistrationCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IUserIdAndOrganizationIdRepository userIdAndOrganizationIdRepository,
        IOtpRepository otpRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _mapper = mapper;
        _userIdAndOrganizationIdRepository = userIdAndOrganizationIdRepository;
        _otpRepository = otpRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        request.Email = request.Email?.ToLower();

        if (request.Email == null && request.PhoneNumber == null)
        {
            return Result<string>.BadRequest("Email address or phone number is required");
        }

        var userOrgInfo = await _userIdAndOrganizationIdRepository.Get();

        // Check if user with this email already exists

        if (request.Email != null)
        {
            var existingUser = await _userManager.Users
                .AnyAsync(x => x.Email.ToLower() == request.Email && x.EmailConfirmed);
            if (existingUser)
            {
                return Result<string>.BadRequest("User with this email already exists.");
            }
        }

        if (request.PhoneNumber != null)
        {
            var existingUser = await _userManager.Users
                .AnyAsync(x => x.PhoneNumber.ToLower() == request.PhoneNumber && x.PhoneNumberConfirmed);
            if (existingUser)
            {
                return Result<string>.BadRequest("User with this email already exists.");
            }
        }

        // Create new user
        var user = new User
        {
            UserName = request.Email.Trim().ToLower(),
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            OrganizationId = userOrgInfo.OrganizationId.Value,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            UserType = UserType.WebUser
        };

        var createUserResult = await _userManager.CreateAsync(user,request.Password);

        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
            return Result<string>.BadRequest($"Failed to create user: {errors}");
        }

        // Generate and save OTP
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var otpEntity = await _otpRepository.GenerateAndAddOtpAsync(user.Id, "Registration", OtpSentOn.Email, cancellationToken);

            // Send OTP email (FIXED)
            await SendRegistrationOtpEmail(
                request.Email,
                otpEntity.Otp,
                $"{request.FirstName ?? ""} {request.LastName ?? ""}".Trim()
            );
        }

        var countAfterCreate = await _userManager.Users.CountAsync();

        return Result<string>.Success("User registered successfully. OTP sent to email.");
    }

    private async Task SendRegistrationOtpEmail(string email, int otp, string name)
    {

        string subject = "OTP Verification";
        string emailBody = $@"
          Dear {name},

          Thank you for registering with us.

         Your OTP for account registration is:

         OTP: {otp}

         This OTP is valid for 5 minutes.
         Please do not share this OTP with anyone.

         If you did not request this registration, please ignore this email.

         Regards,
         Support Team
         ";

        await _emailService.SendEmail(email, subject, emailBody);
    }
}
