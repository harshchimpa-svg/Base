

using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCategorys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymCategorys.Command;

public class UpdateGymCategoryCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
internal class UpdateGymCategoryCommandHandler : IRequestHandler<UpdateGymCategoryCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateGymCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(  UpdateGymCategoryCommand request,  CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Repository<GymCategory>()
            .Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (category == null)
            return Result<string>.BadRequest("Gym Category not found");

        category.Name = request.Name;
        category.Description = request.Description;

        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym Category Updated");
    }
}