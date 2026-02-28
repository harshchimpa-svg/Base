using Application.Dto.Services;
using Application.Dto.ShopeSettings;
using Application.Features.Services.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using Domain.Entities.ShopeSettings;
using MediatR;
using Shared;

namespace Application.Features.ShopeSettings.Queries;

public class GetShopeSettingByIdQuery: IRequest<Result<GetShopSettingDto>>
{
    public int Id { get; set; }

    public GetShopeSettingByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetShopeSettingByIdQueryHandler : IRequestHandler<GetShopeSettingByIdQuery, Result<GetShopSettingDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetShopeSettingByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetShopSettingDto>> Handle(GetShopeSettingByIdQuery request, CancellationToken cancellationToken) 
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