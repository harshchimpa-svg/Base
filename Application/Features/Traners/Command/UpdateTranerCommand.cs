
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper; 
using Domain.Entities.ApplicationUsers;
using Domain.Entities.UserAddresses;
using Domain.Entities.UserProfiles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.ComponentModel.DataAnnotations;
using Domain.Common.Enums.Users.UserRoleType;

namespace Application.Features.Tranners.Commands;

public class UpdateTranerCommand : IRequest<Result<string>>
{
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? OtherDetails { get; set; }

    public string Name { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public UserLevelType UserLevelType  { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }   
    public string? Country { get; set; }
    public int? PinCode { get; set; }
}

internal class UpdateTranerCommandHandler : IRequestHandler<UpdateTranerCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserIdAndOrganizationIdRepository _userOrganization;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTranerCommandHandler(UserManager<User> userManager, IMapper mapper, IUserIdAndOrganizationIdRepository userOrganization, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _mapper = mapper;
        _userOrganization = userOrganization;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateTranerCommand request, CancellationToken cancellationToken)
    {
        var useOrga = await _userOrganization.Get();

        var user = await _userManager.FindByIdAsync(useOrga.UserId);
        if (user == null)
        {
            return Result<string>.NotFound("User not found.");
        }

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
            return Result<string>.BadRequest($"Failed to update user: {errors}");
        }

        if (HasProfileFields(request))
        {
            await UpdateUserProfile(request, user.Id, cancellationToken);
        }

        if (HasAddressFields(request))
        {
            await UpdateUserAddress(request, user.Id, cancellationToken);
        }

        return Result<string>.Success(user.Id, "User updated successfully.");
    }

    private bool HasProfileFields(UpdateTranerCommand request)
    {
        return request.PhoneNumber != null || request.Email != null || request.Weight != null ||
               request.Height != null || request.UserLevelType != null ||
               request.DateOfBirth != null;
    }

    private bool HasAddressFields(UpdateTranerCommand request)
    {
        return request.Address1 != null || request.Address2 != null || request.City != null ||
               request.State != null || request.Country != null || request.PinCode != null;
    }

    private async Task UpdateUserProfile(UpdateTranerCommand request, string userId, CancellationToken cancellationToken)
    {
        var userProfileExists = await _unitOfWork.Repository<UserProfile>()
            .Entities
            .AnyAsync(up => up.UserId == userId, cancellationToken);

        UserProfile userProfile;
        if (!userProfileExists)
        {
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

        if (request.PhoneNumber != null)
        if (request.Height != null)
            userProfile.Height = request.Height;
        if (request.UserLevelType != null)
            userProfile.UserLevelType = request.UserLevelType;
        if (request.DateOfBirth != null)
            userProfile.DateOfBirth = request.DateOfBirth;


        await _unitOfWork.Repository<UserProfile>().UpdateAsync(userProfile);
        await _unitOfWork.Save(cancellationToken);
    }

    private async Task UpdateUserAddress(UpdateTranerCommand request, string userId, CancellationToken cancellationToken)
    {
        var userAddressExists = await _unitOfWork.Repository<UserAddress>()
            .Entities
            .AnyAsync(ua => ua.UserId == userId, cancellationToken);

        UserAddress userAddress;
        if (!userAddressExists)
        {
            userAddress = new UserAddress
            {
                UserId = userId
            };
            await _unitOfWork.Repository<UserAddress>().AddAsync(userAddress);
            await _unitOfWork.Save(cancellationToken);
        }
        else
        {
            userAddress = await _unitOfWork.Repository<UserAddress>()
                .Entities
                .FirstOrDefaultAsync(ua => ua.UserId == userId, cancellationToken);
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
        if (request.PinCode != null)
            userAddress.PinCode = request.PinCode;

        await _unitOfWork.Repository<UserAddress>().UpdateAsync(userAddress);
        await _unitOfWork.Save(cancellationToken);
    }
}
