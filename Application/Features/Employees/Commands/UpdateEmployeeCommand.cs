using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.Users.UserRoleType;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.UserAddresses;
using Domain.Entities.UserProfiles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Employees.Commands;

public class UpdateEmployeeCommand : IRequest<Result<string>>
{
    [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? OtherDetails { get; set; } 

    public int PhoneNumber { get; set; }
    public string? Email { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public UserLevelType UserLevelType { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int? PinCode { get; set; }
}

internal class UpdateEmployeeCommandHandler
    : IRequestHandler<UpdateEmployeeCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserIdAndOrganizationIdRepository _userOrganization;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(
        UserManager<User> userManager,
        IUserIdAndOrganizationIdRepository userOrganization,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _userOrganization = userOrganization;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var userOrg = await _userOrganization.Get();

        var user = await _userManager.FindByIdAsync(userOrg.UserId);
        if (user == null)
            return Result<string>.NotFound("User not found.");

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

        if (HasProfileFields(request))
            await UpdateUserProfile(request, user.Id, cancellationToken);

        if (HasAddressFields(request))
            await UpdateUserAddress(request, user.Id, cancellationToken);

        return Result<string>.Success(user.Id, "Employee updated successfully.");
    }

    private bool HasProfileFields(UpdateEmployeeCommand request)
    {
        return request.PhoneNumber != 0 ||
               request.Weight != 0 ||
               request.Height != 0 ||
               request.UserLevelType != 0 ||
               request.DateOfBirth != default;
    }

    private bool HasAddressFields(UpdateEmployeeCommand request)
    {
        return !string.IsNullOrEmpty(request.Address1) ||
               !string.IsNullOrEmpty(request.Address2) ||
               !string.IsNullOrEmpty(request.City) ||
               !string.IsNullOrEmpty(request.State) ||
               !string.IsNullOrEmpty(request.Country) ||
               request.PinCode != null;
    }

    private async Task UpdateUserProfile(
        UpdateEmployeeCommand request,
        string userId,
        CancellationToken cancellationToken)
    {
        var userProfile = await _unitOfWork.Repository<UserProfile>()
            .Entities
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (userProfile == null)
        {
            userProfile = new UserProfile { UserId = userId };
            await _unitOfWork.Repository<UserProfile>().AddAsync(userProfile);
        }
        
        if (request.Height != 0)
            userProfile.Height = request.Height;
        if (request.Weight != 0)
            userProfile.Weight = request.Weight;
        if (request.UserLevelType != 0)
            userProfile.UserLevelType = request.UserLevelType;
        if (request.DateOfBirth != default)
            userProfile.DateOfBirth = request.DateOfBirth;

        await _unitOfWork.Repository<UserProfile>().UpdateAsync(userProfile);
        await _unitOfWork.Save(cancellationToken);
    }

    private async Task UpdateUserAddress(
        UpdateEmployeeCommand request,
        string userId,
        CancellationToken cancellationToken)
    {
        var userAddress = await _unitOfWork.Repository<UserAddress>()
            .Entities
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (userAddress == null)
        {
            userAddress = new UserAddress { UserId = userId };
            await _unitOfWork.Repository<UserAddress>().AddAsync(userAddress);
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
