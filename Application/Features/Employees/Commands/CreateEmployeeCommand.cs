using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories.Otps;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.Otps;
using Domain.Common.Enums.Users;
using Domain.Common.Enums.Users.UserRoleType;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.UserAddresses;
using Domain.Entities.UserProfiles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Application.Features.Employees.Commands;

public class CreateEmployeeCommand: IRequest<Result<string>>
{
    [Required]
    public string FirstName { get; set; }

    public string? LastName { get; set; }
    public bool IsOtp { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone] 
    public string? PhoneNumber { get; set; }

    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public UserLevelType UserLevelType { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Message { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int? PinCode { get; set; }

    [Required]
    public string Password { get; set; }
}

internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserIdAndOrganizationIdRepository _userIdAndOrganizationIdRepository;
    private readonly IOtpRepository _otpRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(
        UserManager<User> userManager,
        IMapper mapper,
        IUserIdAndOrganizationIdRepository userIdAndOrganizationIdRepository,
        IOtpRepository otpRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
        _userIdAndOrganizationIdRepository = userIdAndOrganizationIdRepository;
        _otpRepository = otpRepository;
        _emailService = emailService;
    }

    public async Task<Result<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        request.Email = request.Email?.ToLower();

        if (request.Email == null && request.PhoneNumber == null)
            return Result<string>.BadRequest("Email or phone number is required");

        var userOrgInfo = await _userIdAndOrganizationIdRepository.Get();

        var user = new User
        {
            UserName = Guid.NewGuid().ToString(),
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            OrganizationId = userOrgInfo.OrganizationId!.Value,
            EmailConfirmed = !request.IsOtp,
            PhoneNumberConfirmed = !request.IsOtp,

            UserType = UserType.WebUser
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result<string>.BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _unitOfWork.Repository<UserAddress>().AddAsync(new UserAddress
        {
            UserId = user.Id,
            Address1 = request.Address1,
            Address2 = request.Address2,
            City = request.City,
            State = request.State,
            Country = request.Country,
            PinCode = request.PinCode
        });

        await _unitOfWork.Repository<UserProfile>().AddAsync(new UserProfile
        {
            UserId = user.Id,
            Weight = request.Weight,
            Height = request.Height,
            UserLevelType = request.UserLevelType,
            DateOfBirth = request.DateOfBirth,
        });

        await _unitOfWork.Save(cancellationToken);

        if (request.IsOtp && request.Email != null)
        {
            var otp = await _otpRepository.GenerateAndAddOtpAsync(
                user.Id, "Registration", OtpSentOn.Email, cancellationToken);

            await _emailService.SendEmail(
                request.Email,
                "OTP Verification",
                $"Hello {request.FirstName},\n\nYour OTP is: {otp.Otp}\n\nThanks,\nSupport Team");
        }

        return request.IsOtp ? Result<string>.Success("User registered. OTP sent.") : Result<string>.Success("User registered successfully.");
    }
}
// Employee.RoleId = "1a916884-1fbb-4cc1-86b5-bc06125fb7f2";
