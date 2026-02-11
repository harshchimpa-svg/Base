using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.Status;
using Domain.Entities.Sales;
using MediatR;
using Shared;

namespace Application.Features.SaleStatus.Command;

public class CreateSaleStatusCommand : IRequest<Result<string>>
{
    public int SaleId { get; set; }
    public StatusType StatusType { get; set; }
}

internal class CreateSaleStatusCommandHandler : IRequestHandler<CreateSaleStatusCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSaleStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateSaleStatusCommand request, CancellationToken cancellationToken)
    {
        var saleRepo = _unitOfWork.Repository<Sale>();

        var sale = await saleRepo.GetByID(request.SaleId);
        if (sale == null)
            return Result<string>.NotFound("Sale not found");

        string? newInvoiceNo = null;

        if (request.StatusType == StatusType.Success)
        {
            var highestInvoice = await saleRepo.MaxAsync(s => s.InvoiceNo);

            if (string.IsNullOrEmpty(highestInvoice))
            {
                newInvoiceNo = "000001";
            }
            else
            {
                int invoice = int.Parse(highestInvoice);
                invoice += 1;
                newInvoiceNo = invoice.ToString("D6");
            }

            sale.InvoiceNo = newInvoiceNo;
            sale.IsPaid = true;
            sale.IsCanceld = false;
        }
        else if (request.StatusType == StatusType.Pending)
        {
            sale.InvoiceNo = null;
            sale.IsPaid = false;
            sale.IsCanceld = false;
        }
        else if (request.StatusType == StatusType.Failed || request.StatusType == StatusType.Cancelled)
        {
            sale.InvoiceNo = null;
            sale.IsPaid = false;
            sale.IsCanceld = true;
        }

        await saleRepo.UpdateAsync(sale);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Sale status processed successfully");
    }


}
