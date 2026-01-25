using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietTypes;
using MediatR;
using Shared;

namespace Application.Features.DietTypes.Command;

public class CreateDietTypeCommands: IRequest<Result<string>>, ICreateMapFrom<DietType>
{
    public string? Name { get; set; }
}
internal class CreateGymCommandHandler : IRequestHandler<CreateDietTypeCommands, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGymCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateDietTypeCommands request, CancellationToken cancellationToken)
    {
        var gym = _mapper.Map<DietType>(request);

        await _unitOfWork.Repository<DietType>().AddAsync(gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym Created Successfully");
        
    }
}