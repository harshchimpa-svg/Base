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

public class GetShopeSettingByIdQueries: IRequest<Result<GetShopeSettingDto>>
{
    public int Id { get; set; }

    public GetShopeSettingByIdQueries(int id)
    {
        Id = id;
    }
}
internal class GetShopeSettingByIdQueriesHandler : IRequestHandler<GetShopeSettingByIdQueries, Result<GetShopeSettingDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetShopeSettingByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetShopeSettingDto>> Handle(GetShopeSettingByIdQueries request, CancellationToken cancellationToken) 
    {
        var Service = await _unitOfWork.Repository<ShopeSetting>().GetByID(request.Id);

        if (Service == null)
        {
            return Result<GetShopeSettingDto>.BadRequest("Service not found.");
        }

        var mapData = _mapper.Map<GetShopeSettingDto>(Service);

        return Result<GetShopeSettingDto>.Success(mapData, "Service");
    }
}