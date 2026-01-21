using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Balances;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Balence.Commands;

public class UpdateBalenceCommand: IRequest<Result<Balance>>
{

    public int Id { get; set; }
    public CreateBalenceCommand CreateCommand { get; set; } = new();

    public UpdateBalenceCommand(int id, CreateBalenceCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateCatagoryesCommandHandler : IRequestHandler<UpdateBalenceCommand, Result<Balance>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCatagoryesCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Balance>> Handle(UpdateBalenceCommand request, CancellationToken cancellationToken)
    {
        var Balance = await _unitOfWork.Repository<Balance>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Balance == null)
        {
            return Result<Balance>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Balance);

        await _unitOfWork.Repository<Balance>().UpdateAsync(Balance);
        await _unitOfWork.Save(cancellationToken);

        return Result<Balance>.Success("Update Balance...");
    }
}