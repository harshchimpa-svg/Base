

using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCategorys;
using MediatR;
using Shared;

namespace Application.Features.GymCategorys.Command;

public class CreateGymCategoryCommand : IRequest<Result<int>>, IMapFrom<GymCategory>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<CreateGymCategoryCommand, GymCategory>();
    }
}
internal class CreateGymCategoryCommandHandler : IRequestHandler<CreateGymCategoryCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGymCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle( CreateGymCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<GymCategory>(request);

        await _unitOfWork.Repository<GymCategory>().AddAsync(category);
        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(category.Id, "Gym Category Created");
    }
}