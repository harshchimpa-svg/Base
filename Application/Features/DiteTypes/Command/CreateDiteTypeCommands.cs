using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DiteTypes;
using MediatR;
using Shared;

namespace Application.Features.DiteTypes.Command;

public class CreateDiteTypeCommands: IRequest<Result<string>>, ICreateMapFrom<DiteType>
{
    public string? Name { get; set; }
}
internal class CreateGymCommandHandler : IRequestHandler<CreateDiteTypeCommands, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGymCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateDiteTypeCommands request, CancellationToken cancellationToken)
    {
        var gym = _mapper.Map<DiteType>(request);

        await _unitOfWork.Repository<DiteType>().AddAsync(gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym Created Successfully");
        
    }
}