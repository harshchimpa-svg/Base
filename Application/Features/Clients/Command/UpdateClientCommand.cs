using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Clientses.Command;

public class UpdateClientCommand: IRequest<Result<Clients>>
{

    public int Id { get; set; }
    public CreateClientCommand CreateCommand { get; set; } = new();

    public UpdateClientCommand(int id, CreateClientCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateClientsCommandHandler : IRequestHandler<UpdateClientCommand, Result<Clients>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientsCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Clients>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {

        var location = await _unitOfWork.Repository<Clients>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (location == null)
        {
            return Result<Clients>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, location);

        await _unitOfWork.Repository<Clients>().UpdateAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<Clients>.Success("Update Clients...");
    }
}