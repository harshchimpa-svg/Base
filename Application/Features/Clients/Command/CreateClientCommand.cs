using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using MediatR;
using Shared;

namespace Application.Features.Clientses.Command;

public class CreateClientCommand: IRequest<Result<string>>, ICreateMapFrom<Clients>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int? ServiceId { get; set; }
    public decimal Quantity  { get; set; }
}

internal class CreateClientsCommandHandler : IRequestHandler<CreateClientCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateClientsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateClientCommand request, CancellationToken cancellationToken) 
    {
        var Clients = _mapper.Map<Clients>(request);

        await _unitOfWork.Repository<Clients>().AddAsync(Clients);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Clients created successfully.");
    }
}