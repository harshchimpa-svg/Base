

using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCategorys;
using MediatR;
using Shared;

namespace Application.Features.GymCategories.Command;

public class CreateGymCategoryCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.GymCategorys.GymCategories>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<CreateGymCategoryCommand, Domain.Entities.GymCategorys.GymCategories>();
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
        var gymCategory = _mapper.Map<Domain.Entities.GymCategorys.GymCategories>(request);

        await _unitOfWork.Repository<Domain.Entities.GymCategorys.GymCategories>().AddAsync(gymCategory);
        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(gymCategory.Id, "Gym Category Created");
    }
}