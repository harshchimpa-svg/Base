using System.Security.Claims;
using Application.Dto.ShopeSettings;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ShopeSettings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ShopeSettings.Queries;

public class GetAllShopSettingQuery : IRequest<Result<List<GetShopSettingDto>>>
{
}

internal class GetAllShopeSettingQueryHandler : IRequestHandler<GetAllShopSettingQuery, Result<List<GetShopSettingDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllShopeSettingQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<List<GetShopSettingDto>>> Handle(GetAllShopSettingQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || !user.Identity!.IsAuthenticated)
            return Result<List<GetShopSettingDto>>.BadRequest("User not authenticated");

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("id")?.Value ?? user.FindFirst("sub")?.Value;

        var role = user.FindFirst(ClaimTypes.Role)?.Value ?? user.FindFirst("role")?.Value;

        if (string.IsNullOrWhiteSpace(userId)) return Result<List<GetShopSettingDto>>.BadRequest("UserId not found in token");
 
        IQueryable<ShopSetting> query = _unitOfWork.Repository<ShopSetting>().Entities;

        if (!string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.UserId == userId);
        }

        var shopSettings = await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<GetShopSettingDto>>(shopSettings);

        return Result<List<GetShopSettingDto>>
            .Success(result, "ShopSetting list");
    }

}
