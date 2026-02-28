using System.Security.Claims;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ShopeSettings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ShopeSettings.Command;

public class UpdateShopeSettingCommand: IRequest<Result<ShopSetting>>
{
    public int Id { get; set; }
    public CreateShopeSettingCommand CreateCommand { get; set; } = new();

    public UpdateShopeSettingCommand(int id, CreateShopeSettingCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}

internal class UpdateShopeSettingCommandHandler : IRequestHandler<UpdateShopeSettingCommand, Result<ShopSetting>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateShopeSettingCommandHandler(
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<ShopSetting>> Handle(UpdateShopeSettingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            ?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
            return Result<ShopSetting>.BadRequest("User not authenticated");

        var shopSetting = await _unitOfWork
            .Repository<ShopSetting>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (shopSetting == null)
            return Result<ShopSetting>.BadRequest("ShopSetting not found or access denied");

        _mapper.Map(request.CreateCommand, shopSetting);

        await _unitOfWork.Repository<ShopSetting>().UpdateAsync(shopSetting);
        await _unitOfWork.Save(cancellationToken);

        return Result<ShopSetting>.Success(shopSetting, "ShopSetting updated successfully");
    }
}
