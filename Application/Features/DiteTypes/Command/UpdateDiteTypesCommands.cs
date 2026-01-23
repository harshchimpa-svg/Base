using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DiteTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DiteTypes.Command;

public class UpdateDiteTypesCommands: IRequest<Result<DiteType>>
{

    public int Id { get; set; }
    public CreateDiteTypeCommands CreateCommand { get; set; } = new();

    public UpdateDiteTypesCommands(int id, CreateDiteTypeCommands createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateDiteTypesCommandsHandler : IRequestHandler<UpdateDiteTypesCommands, Result<DiteType>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiteTypesCommandsHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DiteType>> Handle(UpdateDiteTypesCommands request, CancellationToken cancellationToken)
    {
        var Balance = await _unitOfWork.Repository<DiteType>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Balance == null)
        {
            return Result<DiteType>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Balance);

        await _unitOfWork.Repository<DiteType>().UpdateAsync(Balance);
        await _unitOfWork.Save(cancellationToken);

        return Result<DiteType>.Success("Update Balance...");
    }
}