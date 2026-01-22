

using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;


namespace Application.Features.Gyms.Command;

public class UpdateGymCommand : IRequest<Result<Gym>>
{
    public int Id { get; set; }

    public CreateGymCommand CreateCommand { get; set; } = new();

    public UpdateGymCommand(int id, CreateGymCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}

internal class UpdateGymHandler : IRequestHandler<UpdateGymCommand, Result<Gym>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Result<Gym>> Handle(UpdateGymCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.LocationId.HasValue)
        {
            var location = await _unitOfWork.Repository<Gym>().GetByID(request.CreateCommand.LocationId.Value);
            
            if (location == null)
            {
                return Result<Gym>.BadRequest("Location id is not exist");
            }
        }
        var gym = await _unitOfWork.Repository<Gym>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
        {
            if (gym == null)
            {
                return Result<Gym>.BadRequest("soory id not found");
            }

            _mapper.Map(request.CreateCommand, gym);

            await _unitOfWork.Repository<Gym>().UpdateAsync(gym);
            await _unitOfWork.Save(cancellationToken);

            return Result<Gym>.Success("Updated Gym");
        }
    }
}