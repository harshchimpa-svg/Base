using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymTrainers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymTrainers.Command;

public class UpdateGymTrainerCommand: IRequest<Result<GymTrainer>>
{

    public int Id { get; set; }
    public CreateGymTrainerCommand CreateCommand { get; set; } = new();

    public UpdateGymTrainerCommand(int id, CreateGymTrainerCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateGymTrainerCommandHandler : IRequestHandler<UpdateGymTrainerCommand, Result<GymTrainer>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGymTrainerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GymTrainer>> Handle(UpdateGymTrainerCommand request, CancellationToken cancellationToken)
    {

        var gymTrainer = await _unitOfWork.Repository<GymTrainer>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (gymTrainer == null)
        {
            return Result<GymTrainer>.BadRequest("GymTrainer id not found");
        }

        _mapper.Map(request.CreateCommand, gymTrainer);

        await _unitOfWork.Repository<GymTrainer>().UpdateAsync(gymTrainer);
        await _unitOfWork.Save(cancellationToken);

        return Result<GymTrainer>.Success("Update GymTrainer...");
    }
}