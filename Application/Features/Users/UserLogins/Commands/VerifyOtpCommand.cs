using Application.Features.Commons;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Common.Enums.Otps;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.OTPs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users;

public class VerifyOtpCommand : IRequest<Result<string>>
{
    [Required(ErrorMessage = "Otp is required")]
    public int Otp { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
}

internal class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IJWTService _jwtService;
    private readonly IUserIdAndOrganizationIdRepository _userIdAndOrganizationIdRepository;

    public VerifyOtpCommandHandler(
        IUnitOfWork unitOfWork, 
        UserManager<User> userManager, 
        IJWTService jwtService, 
        IUserIdAndOrganizationIdRepository userIdAndOrganizationIdRepository)
    {
        _userIdAndOrganizationIdRepository = userIdAndOrganizationIdRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<Result<string>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var useOrga = await _userIdAndOrganizationIdRepository.Get();

        User? user = null;

        var username = request.Username.Trim();

        if (!ValidationManager.IsValidPhoneNumber(username))
        {
            if (!username.EndsWith("@"))
            {
                return Result<string>.BadRequest("Only Gmail or phone number is allowed.");
            }

            user = await _userManager.Users
                .Where(x => x.Email == username)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return Result<string>.BadRequest("Email not registered.");
            }
        }
        else
        {
            user = await _userManager.Users
                .Where(x => x.PhoneNumber != null && x.PhoneNumber == username)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return Result<string>.BadRequest("Phone number not registered.");
            }
        }

        OtpSentOn otpSentOn = ValidationManager.IsValidPhoneNumber(username) ? OtpSentOn.PhoneNumber : OtpSentOn.Email;

        var otpEntity = await _unitOfWork.Repository<OTP>().Entities
            .Where(x => x.UserId == user.Id
                        && x.OtpSentOn == otpSentOn
                        && (x.ForOtp == "Registration" || x.ForOtp == "Forgot")
                        && !x.IsChecked)
            .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefaultAsync();

        if (otpEntity == null)
        {
            return Result<string>.BadRequest("No otp requested!");
        }

        if (otpEntity.TimesChecked > 5)
        {
            return Result<string>.BadRequest("You checked otp too many times request new otp");
        }

        if (!otpEntity.CreatedDate.HasValue || (DateTime.UtcNow - otpEntity.CreatedDate.Value).TotalMinutes >= 5)
        {
            return Result<string>.BadRequest("Otp expired request new otp");
        }

        if (otpEntity.Otp != request.Otp)
        {
            otpEntity.TimesChecked += 1;
            await _unitOfWork.Repository<OTP>().UpdateAsync(otpEntity, otpEntity.Id);
            await _unitOfWork.Save(cancellationToken);
            return Result<string>.BadRequest("Incorrect otp!");
        }

        otpEntity.IsChecked = true;
        await _unitOfWork.Repository<OTP>().UpdateAsync(otpEntity, otpEntity.Id);
        await _unitOfWork.Save(cancellationToken);

        if (otpSentOn == OtpSentOn.PhoneNumber && !user.PhoneNumberConfirmed) user.PhoneNumberConfirmed = true;
        else if (otpSentOn == OtpSentOn.Email && !user.EmailConfirmed) user.EmailConfirmed = true;

        await _userManager.UpdateAsync(user);

        var token = await _jwtService.GenerateToken(user.Id);

        return Result<string>.Success(token, "Otp verified successfully", token);
    }
}
