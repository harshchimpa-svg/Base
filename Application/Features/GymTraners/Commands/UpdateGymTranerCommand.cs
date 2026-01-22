/*using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymTraners;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymTraners.Commands;

public class UpdateGymTranerCommand: IRequest<Result<GemTraner>>
{

    public int Id { get; set; }
    public CreateGymTranerCommands CreateCommand { get; set; } = new();

    public UpdateGymTranerCommand(int id, CreateGymTranerCommands createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateGymTranerCommandHandler : IRequestHandler<UpdateGymTranerCommand, Result<GemTraner>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGymTranerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GemTraner>> Handle(UpdateGymTranerCommand request, CancellationToken cancellationToken)
    {

        var Service = await _unitOfWork.Repository<GemTraner>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Service == null)
        {
            return Result<GemTraner>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Service);

        await _unitOfWork.Repository<GemTraner>().UpdateAsync(Service);
        await _unitOfWork.Save(cancellationToken);

        return Result<GemTraner>.Success("Update GemTraner...");
    }
}*/