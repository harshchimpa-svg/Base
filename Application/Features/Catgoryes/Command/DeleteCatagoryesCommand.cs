using Application.Interfaces.UnitOfWorkRepositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagoryes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Catgoryes.Command;

public class DeleteCatagoryesCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteCatagoryesCommand(int id)
    {
        Id = id;
    }
}
internal class DeleteCatagoryesCommandHandler : IRequestHandler<DeleteCatagoryesCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCatagoryesCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteCatagoryesCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Catgory>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Catgory not found.");
        }

        await _unitOfWork.Repository<Catgory>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Catgory deleted successfully.");
    }
}