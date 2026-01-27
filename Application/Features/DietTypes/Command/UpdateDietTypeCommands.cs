using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DietTypes.Command;

public class UpdateDietTypeCommands: IRequest<Result<DietType>>
{

    public int Id { get; set; }
    public CreateDietTypeCommands CreateCommand { get; set; } = new();

    public UpdateDietTypeCommands(int id, CreateDietTypeCommands createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateDietTypesCommandsHandler : IRequestHandler<UpdateDietTypeCommands, Result<DietType>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDietTypesCommandsHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DietType>> Handle(UpdateDietTypeCommands request, CancellationToken cancellationToken)
    {
        var Balance = await _unitOfWork.Repository<DietType>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Balance == null)
        {
            return Result<DietType>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Balance);

        await _unitOfWork.Repository<DietType>().UpdateAsync(Balance);
        await _unitOfWork.Save(cancellationToken);

        return Result<DietType>.Success("Update Balance...");
    }
}