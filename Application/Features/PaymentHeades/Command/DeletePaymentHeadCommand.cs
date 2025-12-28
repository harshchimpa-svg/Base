using Application.Interfaces.UnitOfWorkRepositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.PaymentHeates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.PaymentHeades.Command;

public class DeletePaymentHeadCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeletePaymentHeadCommand(int id)
    {
        Id = id;
    }
}
internal class DeletePaymentHeadCommandHandler : IRequestHandler<DeletePaymentHeadCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePaymentHeadCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeletePaymentHeadCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<PaymentHead>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("PaymentHead not found.");
        }

        await _unitOfWork.Repository<PaymentHead>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "PaymentHead deleted successfully.");
    }
}