using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Gyms;
using Domain.Entities.GymTraners;
using MediatR;
using Shared;

namespace Application.Features.GymTraners.Command;

public class CreateGymTranerCommand: IRequest<Result<string>>, ICreateMapFrom<GemTraner>
{
    public string UserId { get; set; }
    public int GymId { get; set; }

}

internal class CreateGymTranerCommandHandler : IRequestHandler<CreateGymTranerCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateGymTranerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateGymTranerCommand request, CancellationToken cancellationToken)
    {
        var gemTraner = _mapper.Map<GemTraner>(request);

        await _unitOfWork.Repository<GemTraner>().AddAsync(gemTraner);
        await _unitOfWork.Save(cancellationToken);
        
        return Result<string>.Success("GemTraner created successfully.");
    }
}