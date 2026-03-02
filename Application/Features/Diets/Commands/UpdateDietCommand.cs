using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Diets;
using Domain.Entities.DietTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Diets.Commands;

public class UpdateDietCommand: IRequest<Result<Diet>>
{

    public int Id { get; set; }
    public CreateDietCommand CreateCommand { get; set; } = new();

    public UpdateDietCommand(int id, CreateDietCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateDietCommandHandler : IRequestHandler<UpdateDietCommand, Result<Diet>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDietCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Diet>> Handle(UpdateDietCommand request, CancellationToken cancellationToken)
    { 
        var dietTypeExists = await _unitOfWork.Repository<DietType>().GetByID(request.CreateCommand.DietTypeId);

        if (dietTypeExists == null)
        {
            return Result<Diet>.BadRequest("DietType does not exist.");
        }

        var diet = await _unitOfWork.Repository<Diet>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (diet == null)
        {
            return Result<Diet>.BadRequest("Diet id not found.");
        }
        _mapper.Map(request.CreateCommand, diet);

        await _unitOfWork.Repository<Diet>().UpdateAsync(diet);
        await _unitOfWork.Save(cancellationToken);

        return Result<Diet>.Success("Diet updated successfully.");
    }
}