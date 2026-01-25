using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Services.Command;

public class UpdateServiceCommand: IRequest<Result<Service>>
{

    public int Id { get; set; }
    public CreateServiceCommand CreateCommand { get; set; } = new();

    public UpdateServiceCommand(int id, CreateServiceCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateServicesCommandHandler : IRequestHandler<UpdateServiceCommand, Result<Service>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServicesCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Service>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {

        var Service = await _unitOfWork.Repository<Service>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Service == null)
        {
            return Result<Service>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Service);

        await _unitOfWork.Repository<Service>().UpdateAsync(Service);
        await _unitOfWork.Save(cancellationToken);

        return Result<Service>.Success("Update Service...");
    }
}