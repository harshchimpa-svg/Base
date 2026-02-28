using Application.Features.Services.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Services;
using Domain.Entities.ShopeSettings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ShopeSettings.Command;

public class DeleteShopeSettingCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteShopeSettingCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteShopeSettingCommandHandler : IRequestHandler<DeleteShopeSettingCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteShopeSettingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteShopeSettingCommand request, CancellationToken cancellationToken)
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