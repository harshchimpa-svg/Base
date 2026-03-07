using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Vendors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Vendors.Command;

public class DeleteVendorCommand: IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteVendorCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVendorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        var vendorExists = await _unitOfWork.Repository<Vendor>().Entities
            .AnyAsync(x => x.Id == request.Id);

        if (!vendorExists)
        {
            return Result<bool>.BadRequest("Vendors not found.");
        }
        await _unitOfWork.Repository<Vendor>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Vendors deleted successfully.");
    }
}