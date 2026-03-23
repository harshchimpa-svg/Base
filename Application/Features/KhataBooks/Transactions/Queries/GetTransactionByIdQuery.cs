using Application.Dto.Balances;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Transactions;
using MediatR;
using Shared;

namespace Application.Features.Balance.Queries;

public class GetTransactionByIdQuery: IRequest<Result<GetTransactionDto>>
{
    public int Id { get; set; }

    public GetTransactionByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Result<GetTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTransactionByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<GetTransactionDto>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.Repository<Transaction>().GetByID(request.Id);

        if (transaction == null)
        {
            return Result<GetTransactionDto>.BadRequest("Transaction not found.");
        }

        var mapData = _mapper.Map<GetTransactionDto>(transaction);

        return Result<GetTransactionDto>.Success(mapData, "Transaction");
    }
}