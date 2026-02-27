using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Sales.Command;

public class UpdateSaleCommand: IRequest<Result<Sale>>
{

    public int Id { get; set; }
    public CreateSaleCommand CreateCommand { get; set; } = new();

    public UpdateSaleCommand(int id, CreateSaleCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Result<Sale>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSaleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Sale>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var Sale = await _unitOfWork.Repository<Sale>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Sale == null)
        {
            return Result<Sale>.BadRequest("Sale id not found");
        }

        _mapper.Map(request.CreateCommand, Sale);

        await _unitOfWork.Repository<Sale>().UpdateAsync(Sale);
        await _unitOfWork.Save(cancellationToken);

        return Result<Sale>.Success("Update Sale...");
    }
}