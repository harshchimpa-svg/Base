

using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Gyms;
using MediatR;
using Shared;


namespace Application.Features.Gyms.Command;

public class CreateGymCommand : IRequest<Result<string>>, ICreateMapFrom<Gym>
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int? LocationId { get; set; }
}

internal class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGymCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        if (request.LocationId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<Gym>().GetByID(request.LocationId.Value);
            if (parentExists == null)
            {
                return Result<string>.BadRequest("Location id not exit");
            }
        }

        var gym = _mapper.Map<Gym>(request);

        await _unitOfWork.Repository<Gym>().AddAsync(gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym Created Successfully");
        
    }
}