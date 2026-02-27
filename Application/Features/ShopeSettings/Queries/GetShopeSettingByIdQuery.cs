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

public class GetShopeSettingByIdQuery: IRequest<Result<GetShopeSettingDto>>
{
    public int Id { get; set; }

    public GetShopeSettingByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetShopeSettingByIdQueryHandler : IRequestHandler<GetShopeSettingByIdQuery, Result<GetShopeSettingDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetShopeSettingByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetShopeSettingDto>> Handle(GetShopeSettingByIdQuery request, CancellationToken cancellationToken) 
    {
        var service = await _unitOfWork.Repository<ShopeSetting>().GetByID(request.Id);

        if (service == null)
        {
            return Result<GetShopeSettingDto>.BadRequest("Service not found.");
        }

        var mapData = _mapper.Map<GetShopeSettingDto>(service);

        return Result<GetShopeSettingDto>.Success(mapData, "Service");
    }
}