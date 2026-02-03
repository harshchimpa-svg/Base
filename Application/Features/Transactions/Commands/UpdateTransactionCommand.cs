using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Commands;

public class UpdateTransactionCommand: IRequest<Result<Transaction>>
{

    public int Id { get; set; }
    public CreateTransactionCommand CreateCommand { get; set; } = new();

    public UpdateTransactionCommand(int id, CreateTransactionCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateCatagoryesCommandHandler : IRequestHandler<UpdateTransactionCommand, Result<Transaction>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCatagoryesCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Transaction>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var Transaction = await _unitOfWork.Repository<Transaction>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Transaction == null)
        {
            return Result<Transaction>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Transaction);

        await _unitOfWork.Repository<Transaction>().UpdateAsync(Transaction);
        await _unitOfWork.Save(cancellationToken);

        return Result<Transaction>.Success("Update Transaction...");
    }
}