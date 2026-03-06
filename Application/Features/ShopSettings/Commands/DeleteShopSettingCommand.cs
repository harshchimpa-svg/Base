using Application.Features.Services.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Services;
using Domain.Entities.ShopSettings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ShopSettings.Command;

public class DeleteShopSettingCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteShopSettingCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteshopSettingCommandHandler : IRequestHandler<DeleteShopSettingCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteshopSettingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteShopSettingCommand request, CancellationToken cancellationToken)
    {
        var shopSetting = await _unitOfWork.Repository<ShopSetting>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!shopSetting)
        {
            return Result<bool>.BadRequest("ShopSetting not found.");
        }

        await _unitOfWork.Repository<ShopSetting>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "ShopSetting deleted successfully.");
    }
}