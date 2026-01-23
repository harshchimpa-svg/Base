using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Dites;
using Domain.Entities.DiteTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Dites.Commands;

public class UpdateDiteCommand: IRequest<Result<diet>>
{

    public int Id { get; set; }
    public CreateDiteCommand CreateCommand { get; set; } = new();

    public UpdateDiteCommand(int id, CreateDiteCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateDiteCommandHandler : IRequestHandler<UpdateDiteCommand, Result<diet>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<diet>> Handle(UpdateDiteCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.DiteTypeId.HasValue)
        {
            var parent = await _unitOfWork.Repository<diet>().GetByID(request.CreateCommand.DiteTypeId.Value);

            if (parent == null)
            {
                return Result<diet>.BadRequest("HouseId is not exist.");
            }
        }
        var location = await _unitOfWork.Repository<diet>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (location == null)
        {
            return Result<diet>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, location);

        await _unitOfWork.Repository<diet>().UpdateAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<diet>.Success("Update Clients...");
    }
}