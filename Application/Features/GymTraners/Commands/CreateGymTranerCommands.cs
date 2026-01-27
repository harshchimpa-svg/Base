using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Gyms;
using Domain.Entities.GymTraners;
using MediatR;
using Shared;

namespace Application.Features.GymTraners.Commands;

public class CreateGymTranerCommands: IRequest<Result<string>>, ICreateMapFrom<GemTraner>
{
    public string UserId { get; set; }
    public int GymId { get; set; }

}

internal class CreateGymTranerCommandsHandler : IRequestHandler<CreateGymTranerCommands, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateGymTranerCommandsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateGymTranerCommands request, CancellationToken cancellationToken)
    {
        var GemTraner = _mapper.Map<GemTraner>(request);

        await _unitOfWork.Repository<GemTraner>().AddAsync(GemTraner);
        await _unitOfWork.Save(cancellationToken);
        
        return Result<string>.Success("GemTraner created successfully.");
    }
}