using System.Security.Claims;
using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ShopeSettings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.ShopeSettings.Command;

public class CreateShopeSettingCommand: IRequest<Result<string>>, ICreateMapFrom<ShopeSetting>
{
    public string ShopeName { get; set; }
    public string OnerName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public int GstNumber  { get; set; }
    public int? EmployeeId { get; set; }
}

internal class CreateShopeSettingCommandHandler : IRequestHandler<CreateShopeSettingCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;   

    public CreateShopeSettingCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)   
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;  
    }

    public async Task<Result<string>> Handle(CreateShopeSettingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            ?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
            return Result<string>.BadRequest("ShopeSetting not authenticated");

        var ShopeSetting = _mapper.Map<ShopeSetting>(request);

        ShopeSetting.UserId = userId;  

        await _unitOfWork.Repository<ShopeSetting>().AddAsync(ShopeSetting);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("ShopeSetting created successfully.");
    }
}