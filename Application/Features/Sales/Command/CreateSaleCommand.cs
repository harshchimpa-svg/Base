using Application.Common.Mappings.Commons;
using Application.Dto.SalePayments;
using Application.Dto.SaleProducts;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums;
using Domain.Common.Enums.Status;
using Domain.Entities.SalePayments;
using Domain.Entities.SaleProducts;
using Domain.Entities.Sales;
using MediatR;
using Shared;

namespace Application.Features.Sales.Command;

public class CreateSaleCommand : IRequest<Result<string>>, ICreateMapFrom<Sale>
{
    public string? UserId { get; set; }
    public string? InvoiceNo { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }
    public List<GetSaleProductDto> SaleProducts { get; set; }
    public List<GetSalePaymentDto> SalePayments { get; set; }

    public record GetSaleProductDto(string ProductId, string ProductName, decimal Price, decimal Discount, decimal taxe, int Quantity);


    public record GetSalePaymentDto(decimal NetAmount, MethodType MethodType, DateTime PaymentDate);

}

internal class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var Sale = _mapper.Map<Sale>(request);

        await _unitOfWork.Repository<Sale>().AddAsync(Sale);
        await _unitOfWork.Save(cancellationToken);
        await CreateSale(Sale.Id, request, cancellationToken);
        return Result<string>.Success("Diet created successfully.");

    }
    
    private async Task CreateSale(int saleId, CreateSaleCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.SaleProducts)
        {
            var saleProduct = new SaleProduct
            {
                SaleId = saleId,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount,
                taxe = item.taxe
            };

            await _unitOfWork.Repository<SaleProduct>().AddAsync(saleProduct);
        }

        await _unitOfWork.Save(cancellationToken);
    }


    private async Task CreateSalePayments(int saleId, List<GetSalePaymentDto> salePayments, CancellationToken cancellationToken)
    {
        foreach (var item in salePayments)
        {
            var salePayment = new SalePayment
            {
                SaleId = saleId,
                NetAmount = item.NetAmount,
                MethodType = item.MethodType,
                PaymentDate = item.PaymentDate,
                StatusType = StatusType.Success
            };

            await _unitOfWork.Repository<SalePayment>().AddAsync(salePayment);
        }

        await _unitOfWork.Save(cancellationToken);
    }
 }

