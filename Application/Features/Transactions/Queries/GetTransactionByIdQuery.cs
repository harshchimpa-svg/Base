using Application.Dto.Balences;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Transactions;
using MediatR;
using Shared;

namespace Application.Features.Balence.Queries;

public class GetTransactionByIdQuery: IRequest<Result<GetTransactionDto>>
{
    public int Id { get; set; }

    public GetTransactionByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetBalenceByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Result<GetTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetBalenceByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<GetTransactionDto>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Transaction>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetTransactionDto>.BadRequest("Location not found.");
        }

        var mapData = _mapper.Map<GetTransactionDto>(location);

        return Result<GetTransactionDto>.Success(mapData, "Location");
    }
}