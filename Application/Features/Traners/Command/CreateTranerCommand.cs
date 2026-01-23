using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings.Commons;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.Users;
using Domain.Common.Enums.Users.UserRoleType;
using Domain.Entities.ApplicationUsers; using Domain.Entities.UserAddresses;
using Domain.Entities.UserProfiles;
using Domain.Entities.Users.UserRoles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Application.Features.Tranners.Commands;

public class CreateTranerCommand : IRequest<Result<string>>
{  
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public UserType UserType { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public string? OtherDetails { get; set; }
    
    
    public string Email { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public UserLevelType UserLevelType  { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string message { get; set; }
    

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public int? CityId { get; set; }
    public int? StateId { get; set; }
    public int? CountryId { get; set; }
    public int? PinCode { get; set; }
}

internal class CreateTrannerCommandHandler : IRequestHandler<CreateTranerCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IUserIdAndOrganizationIdRepository _userIdAndOrganizationIdRepository;
    private readonly IMapper _mapper;

    public CreateTrannerCommandHandler(IUnitOfWork unitOfWork,UserManager<User> userManager,IUserIdAndOrganizationIdRepository userIdAndOrganizationIdRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userIdAndOrganizationIdRepository = userIdAndOrganizationIdRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateTranerCommand request, CancellationToken cancellationToken)  
    { 
        var userOrgInfo = await _userIdAndOrganizationIdRepository.Get();

        var user = new User
        {
            UserName = Guid.NewGuid().ToString(),
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            OrganizationId = userOrgInfo.OrganizationId!.Value,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            UserType = UserType.WebUser
        };

        var createUserResult = await _userManager.CreateAsync(user);
        
        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
            return Result<string>.BadRequest($"Failed to create user: {errors}");
        }
        
        var userProfile = new UserProfile
        {
            UserId = user.Id,
            Weight = request.Weight,
            Height = request.Height,
            UserLevelType  = request.UserLevelType,
            DateOfBirth = request.DateOfBirth,
            message = request.message,
        };
        
        await _unitOfWork.Repository<UserProfile>().AddAsync(userProfile);
        await _unitOfWork.Save(cancellationToken); 
        
        var UserAddress = new UserAddress
        {
            UserId = user.Id,
            Address1 = request.Address1,
            Address2 = request.Address2,
            CityId  = request.CityId,
            StateId = request.StateId,
            CountryId = request.CountryId,
            PinCode = request.PinCode,
        };
        await _unitOfWork.Repository<UserAddress>().AddAsync(UserAddress);
        await _unitOfWork.Save(cancellationToken); 
        
        
        
        return Result<string>.Success("User registered successfully. OTP sent to email.");
    }
}