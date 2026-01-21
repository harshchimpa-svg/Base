using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Balances;
using MediatR;
using Shared;

namespace Application.Features.Balence.Commands;

public class CreateBalenceCommand: IRequest<Result<string>>, ICreateMapFrom<Balance>
{
    public string? UserId { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
}
internal class CreateBalenceCommandHandler : IRequestHandler<CreateBalenceCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBalenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateBalenceCommand request, CancellationToken cancellationToken)  
    {
        var Balance = _mapper.Map<Balance>(request);

        await _unitOfWork.Repository<Balance>().AddAsync(Balance);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Balance created successfully.");
    }
}