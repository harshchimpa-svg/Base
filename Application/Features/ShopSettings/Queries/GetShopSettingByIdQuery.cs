using Application.Dto.Services;
using Application.Dto.ShopSettings;
using Application.Features.Services.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using Domain.Entities.ShopSettings;
using MediatR;
using Shared;

namespace Application.Features.ShopSettings.Queries;

public class GetShopSettingByIdQuery: IRequest<Result<GetShopSettingDto>>
{
    public int Id { get; set; }

    public GetShopSettingByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetshopSettingByIdQueryHandler : IRequestHandler<GetShopSettingByIdQuery, Result<GetShopSettingDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetshopSettingByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetShopSettingDto>> Handle(GetShopSettingByIdQuery request, CancellationToken cancellationToken) 
    {
        var service = await _unitOfWork.Repository<ShopSetting>().GetByID(request.Id);

        if (service == null)
        {
            return Result<GetShopSettingDto>.BadRequest("Service not found.");
        }

        var mapData = _mapper.Map<GetShopSettingDto>(service);

        return Result<GetShopSettingDto>.Success(mapData, "Service");
    }
}