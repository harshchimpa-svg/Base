using System.Security.Claims;
using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ShopSettings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.ShopSettings.Command;

public class CreateShopSettingCommand: IRequest<Result<string>>, ICreateMapFrom<ShopSetting>
{
    public string ShopeName { get; set; }
    public string OnerName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public int GstNumber  { get; set; }
    public int? EmployeeId { get; set; }
}

internal class CreateshopSettingCommandHandler : IRequestHandler<CreateShopSettingCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;   

    public CreateshopSettingCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)   
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;  
    }

    public async Task<Result<string>> Handle(CreateShopSettingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            ?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
            return Result<string>.BadRequest("ShopSetting not authenticated");

        var shopSetting = _mapper.Map<ShopSetting>(request);

        shopSetting.UserId = userId;  

        await _unitOfWork.Repository<ShopSetting>().AddAsync(shopSetting);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("ShopSetting created successfully.");
    }
}