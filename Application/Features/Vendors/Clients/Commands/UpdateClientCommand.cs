using Application.Features.Clients.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Clients.Command;

public class UpdateClientCommand: IRequest<Result<Client>>
{

    public int Id { get; set; }
    public CreateClientCommand CreateCommand { get; set; } = new();

    public UpdateClientCommand(int id, CreateClientCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<Client>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Client>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {

        var clients = await _unitOfWork.Repository<Client>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (clients == null)
        {
            return Result<Client>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, clients);

        await _unitOfWork.Repository<Client>().UpdateAsync(clients);
        await _unitOfWork.Save(cancellationToken);

        return Result<Client>.Success("Update clients...");
    }
}