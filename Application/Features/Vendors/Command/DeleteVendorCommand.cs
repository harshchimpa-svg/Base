using Application.Features.Categoryes.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Catagories;
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
internal class DDeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DDeleteVendorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<bool>> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Vendor>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Vendors deleted successfully.");
    }
}