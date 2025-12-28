using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.PaymentHeates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.PaymentHeades.Command;

public class UpdatePaymentHeadCommand : IRequest<Result<PaymentHead>>
{

    public int Id { get; set; }
    public CreatePaymentHeadCommand CreateCommand { get; set; } = new();

    public UpdatePaymentHeadCommand(int id, CreatePaymentHeadCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateLocationCommandHandler : IRequestHandler<UpdatePaymentHeadCommand, Result<PaymentHead>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaymentHead>> Handle(UpdatePaymentHeadCommand request, CancellationToken cancellationToken)
    {

        var location = await _unitOfWork.Repository<PaymentHead>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (location == null)
        {
            return Result<PaymentHead>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, location);

        await _unitOfWork.Repository<PaymentHead>().UpdateAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<PaymentHead>.Success("Update location...");
    }
}