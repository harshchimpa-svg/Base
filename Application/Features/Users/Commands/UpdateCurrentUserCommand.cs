using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.Employees;
using Domain.Commons.Enums.Users;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.UserAddresses;
using Domain.Entities.UserProfiles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Services;
using Domain.Common.Enums.Users.UserRoleType;

namespace Application.Features.Users.Commands;

public class UpdateCurrentUserCommand : IRequest<Result<string>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? OtherDetails { get; set; }

    // Profile fields
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public UserLevelType? UserLevelType { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Message { get; set; }

    // Address fields
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int? PinCode { get; set; }

    public IFormFile? ProfileImage { get; set; }
}

internal class UpdateCurrentUserCommandHandler 
    : IRequestHandler<UpdateCurrentUserCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IFileService _fileService;
    private readonly IUserIdAndOrganizationIdRepository _userOrganization;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCurrentUserCommandHandler(
        UserManager<User> userManager,
        IUserIdAndOrganizationIdRepository userOrganization,
        IUnitOfWork unitOfWork,
        IFileService fileService)
    {
        _userManager = userManager;
        _userOrganization = userOrganization;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Result<string>> Handle(
        UpdateCurrentUserCommand request,
        CancellationToken cancellationToken)
    {
        var useOrga = await _userOrganization.Get();
        var user = await _userManager.FindByIdAsync(useOrga.UserId);

        if (user == null)
            return Result<string>.NotFound("User not found.");

        // 🔹 Update User table
        if (request.FirstName != null)
            user.FirstName = request.FirstName;

        if (request.LastName != null)
            user.LastName = request.LastName;

        if (request.OtherDetails != null)
            user.OtherDetails = request.OtherDetails;

        var updateUserResult = await _userManager.UpdateAsync(user);
        if (!updateUserResult.Succeeded)
        {
            var errors = string.Join(", ", updateUserResult.Errors.Select(e => e.Description));
            return Result<string>.BadRequest(errors);
        }

        // 🔹 Update Profile
        await UpdateUserProfile(request, user.Id, cancellationToken);

        // 🔹 Update Address
        await UpdateUserAddress(request, user.Id, cancellationToken);

        return Result<string>.Success(user.Id, "User updated successfully");
    }

    // ================= PROFILE ==================
    private async Task UpdateUserProfile(
        UpdateCurrentUserCommand request,
        string userId,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<UserProfile>();

        var userProfile = await repo.Entities
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (userProfile == null)
        {
            userProfile = new UserProfile
            {
                UserId = userId
            };
            await repo.AddAsync(userProfile);
        }

        if (request.PhoneNumber != null)
            userProfile.PhoneNumber = request.PhoneNumber;

        if (request.Height.HasValue)
            userProfile.Height = request.Height.Value;

        if (request.Weight.HasValue)
            userProfile.Weight = request.Weight.Value;

        if (request.UserLevelType.HasValue)
            userProfile.UserLevelType = request.UserLevelType.Value;

        if (request.DateOfBirth.HasValue)
            userProfile.DateOfBirth = request.DateOfBirth.Value;

        if (request.Message != null)
            userProfile.Message = request.Message;

        if (request.ProfileImage != null)
        {
            var imagePath = await _fileService.UploadAsync(
                request.ProfileImage,
                "UserProfiles");

            userProfile.ProfileImageUrl = imagePath;
        }

        await repo.UpdateAsync(userProfile);
        await _unitOfWork.Save(cancellationToken);
    }

    private async Task UpdateUserAddress(
        UpdateCurrentUserCommand request,
        string userId,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<UserAddress>();

        var userAddress = await repo.Entities
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (userAddress == null)
        {
            userAddress = new UserAddress
            {
                UserId = userId
            };
            await repo.AddAsync(userAddress);
        }

        if (request.Address1 != null)
            userAddress.Address1 = request.Address1;

        if (request.Address2 != null)
            userAddress.Address2 = request.Address2;

        if (request.City != null)
            userAddress.City = request.City;

        if (request.State != null)
            userAddress.State = request.State;

        if (request.Country != null)
            userAddress.Country = request.Country;

        if (request.PinCode.HasValue)
            userAddress.PinCode = request.PinCode.Value;

        await repo.UpdateAsync(userAddress);
        await _unitOfWork.Save(cancellationToken);
    }
}
