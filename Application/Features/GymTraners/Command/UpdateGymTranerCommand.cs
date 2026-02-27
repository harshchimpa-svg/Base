using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymTraners;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymTraners.Command;

public class UpdateGymTranerCommand: IRequest<Result<GemTraner>>
{

    public int Id { get; set; }
    public CreateGymTranerCommand CreateCommand { get; set; } = new();

    public UpdateGymTranerCommand(int id, CreateGymTranerCommand createCommand)
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

        var service = await _unitOfWork.Repository<GemTraner>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (service == null)
        {
            return Result<GemTraner>.BadRequest("gym id not found");
        }

        _mapper.Map(request.CreateCommand, service);

        await _unitOfWork.Repository<GemTraner>().UpdateAsync(service);
        await _unitOfWork.Save(cancellationToken);

        return Result<GemTraner>.Success("Update GemTraner...");
    }
}