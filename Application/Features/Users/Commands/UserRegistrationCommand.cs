using Application.Common.Mappings.Commons;
using Application.Interfaces.Repositories.Otps;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Common.Enums.Otps;
using Domain.Common.Enums.Users;
using Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Shared;
using System.ComponentModel.DataAnnotations;
using Domain.Common.Enums.Users.UserRoleType;
using Domain.Entities.UserProfiles;

namespace Application.Features.Users.Commands;

public class UserRegistrationCommand : IRequest<Result<string>>, ICreateMapFrom<User>
{
    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters")]
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public IFormFile? Image { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
    
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public UserRoleType UserRoleType  { get; set; }
    public decimal age { get; set; }
    public string message { get; set; }

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

    public UserRegistrationCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IUserIdAndOrganizationIdRepository userIdAndOrganizationIdRepository,
        IOtpRepository otpRepository,
        IEmailService emailService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _userIdAndOrganizationIdRepository = userIdAndOrganizationIdRepository;
        _otpRepository = otpRepository;
        _emailService = emailService;
    }

    public async Task<Result<string>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        request.Email = request.Email?.ToLower();

        if (request.Email == null && request.PhoneNumber == null)
            return Result<string>.BadRequest("Email address or phone number is required");

        var userOrgInfo = await _userIdAndOrganizationIdRepository.Get();

        if (request.Email != null)
        {
            var existingUser = await _userManager.Users
                .AnyAsync(x => x.Email.ToLower() == request.Email && x.EmailConfirmed);
            if (existingUser)
                return Result<string>.BadRequest("User with this email already exists.");
        }

        if (request.PhoneNumber != null)
        {
            var existingUser = await _userManager.Users
                .AnyAsync(x => x.PhoneNumber == request.PhoneNumber && x.PhoneNumberConfirmed);
            if (existingUser)
                return Result<string>.BadRequest("User with this phone number already exists.");
        }

        string? imageUrl = null;
        if (request.Image != null)
        {
            var folderPath = Path.Combine("wwwroot", "users");
            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await request.Image.CopyToAsync(stream, cancellationToken);

            imageUrl = $"/users/{fileName}";
        }


        var user = new User
        {
            UserName = Guid.NewGuid().ToString(),
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ImageUrl = imageUrl,
            OrganizationId = userOrgInfo.OrganizationId!.Value,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            UserType = UserType.WebUser
        };

        var createUserResult = await _userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
            return Result<string>.BadRequest($"Failed to create user: {errors}");
        }

        if (request.Email != null)
        {
            var otpEntity = await _otpRepository.GenerateAndAddOtpAsync(
                user.Id,
                "Registration",
                OtpSentOn.Email,
                cancellationToken);

            await SendRegistrationOtpEmail(request.Email, otpEntity.Otp, request.FirstName + " " + request.LastName);
        }

        return Result<string>.Success("User registered successfully. OTP sent to email.");
    }
    public async Task<Result<string>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var userProfileExists = await _unitOfWork.Repository<UserProfile>()
            .Entities
            .AnyAsync(up => up.UserId == userId, cancellationToken);

        UserProfile userProfile;
        if (!userProfileExists)
        {
            // Create new profile if it doesn't exist
            userProfile = new UserProfile
            {
                UserId = userId
            };
            await _unitOfWork.Repository<UserProfile>().AddAsync(userProfile);
            await _unitOfWork.Save(cancellationToken);
        }
        else
        {
            userProfile = await _unitOfWork.Repository<UserProfile>()
                .Entities
                .FirstOrDefaultAsync(up => up.UserId == userId, cancellationToken);
        }

        // Update only non-null profile fields
        if (request.Gender != null)
            userProfile.Gender = request.Gender;
        if (request.DOB != null)
            userProfile.DOB = request.DOB;
        if (request.MaritalStatus != null)
            userProfile.MaritalStatus = request.MaritalStatus;
        if (request.FacebookId != null)
            userProfile.FacebookId = request.FacebookId;
        if (request.LinkedInId != null)
            userProfile.LinkedInId = request.LinkedInId;
        if (request.InstagramId != null)
            userProfile.InstagramId = request.InstagramId;

        await _unitOfWork.Repository<UserProfile>().UpdateAsync(userProfile);
        await _unitOfWork.Save(cancellationToken);
    }
    private async Task SendRegistrationOtpEmail(string email, int otp, string name)
    {
        string subject = "OTP Verification";
        string emailBody =
            $"Hello {name},\n\n" +
            $"Your OTP for registration is: {otp}\n\n" +
            $"Please do not share this OTP with anyone.\n\n" +
            $"Thanks,\nSupport Team";

        await _emailService.SendEmail(email, subject, emailBody);
    }
}
