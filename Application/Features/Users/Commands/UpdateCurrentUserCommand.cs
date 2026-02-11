using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.UserAddresses;
using Domain.Entities.UserProfiles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Application.Interfaces.Services;
using Domain.Common.Enums.Users.UserRoleType;

namespace Application.Features.Users.Commands;

public class UpdateCurrentUserCommand : IRequest<Result<string>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? OtherDetails { get; set; }

    // Profile
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public UserLevelType? UserLevelType { get; set; }
    public DateTime? DateOfBirth { get; set; }

    // Address
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
    private readonly IUserIdAndOrganizationIdRepository _userOrganization;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

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
            return Result<string>.NotFound("User not found");

        // 🔹 USER TABLE
        if (request.FirstName != null)
            user.FirstName = request.FirstName;

        if (request.LastName != null)
            user.LastName = request.LastName;

        if (request.OtherDetails != null)
            user.OtherDetails = request.OtherDetails;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<string>.BadRequest(errors);
        }

        var imageUrl = await UpdateUserProfile(request, user.Id, cancellationToken);

        await UpdateUserAddress(request, user.Id, cancellationToken);

        return Result<string>.Success(imageUrl ?? "", "User updated successfully");
    }

    // ================= PROFILE =================
    private async Task<string?> UpdateUserProfile(
        UpdateCurrentUserCommand request,
        string userId,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<UserProfile>();

        var profile = await repo.Entities
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (profile == null)
        {
            profile = new UserProfile { UserId = userId };
            await repo.AddAsync(profile);
        }

        if (request.Height.HasValue)
            profile.Height = request.Height.Value;

        if (request.Weight.HasValue)
            profile.Weight = request.Weight.Value;

        if (request.UserLevelType.HasValue)
            profile.UserLevelType = request.UserLevelType.Value;

        if (request.DateOfBirth.HasValue)
            profile.DateOfBirth = request.DateOfBirth.Value;

        if (request.ProfileImage != null)
        {
            var path = await _fileService.UploadAsync(
                request.ProfileImage,
                "UserProfiles");

            profile.ProfileImageUrl = path;
        }

        await repo.UpdateAsync(profile);
        await _unitOfWork.Save(cancellationToken);

        return profile.ProfileImageUrl;
    }

    // ================= ADDRESS =================
    private async Task UpdateUserAddress(
        UpdateCurrentUserCommand request,
        string userId,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<UserAddress>();

        var address = await repo.Entities
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (address == null)
        {
            address = new UserAddress { UserId = userId };
            await repo.AddAsync(address);
        }

        if (request.Address1 != null)
            address.Address1 = request.Address1;

        if (request.Address2 != null)
            address.Address2 = request.Address2;

        if (request.City != null)
            address.City = request.City;

        if (request.State != null)
            address.State = request.State;

        if (request.Country != null)
            address.Country = request.Country;

        if (request.PinCode.HasValue)
            address.PinCode = request.PinCode.Value;

        await repo.UpdateAsync(address);
        await _unitOfWork.Save(cancellationToken);
    }
}
