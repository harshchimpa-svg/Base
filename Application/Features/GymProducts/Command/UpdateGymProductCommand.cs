
using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymProducts;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymProducts.Command;

public class UpdateGymProductCommand : IRequest<Result<GymProduct>>
{
    public int Id { get; set; }

    public CreateGymProductCommand CreateCommand { get; set; } = new();

    public UpdateGymProductCommand(int id, CreateGymProductCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateGymProductHandler : IRequestHandler<UpdateGymProductCommand, Result<GymProduct>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGymProductHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GymProduct>> Handle(UpdateGymProductCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.CategoryId.HasValue)
        {
            var category = await _unitOfWork.Repository<GymProduct>().GetByID(request.CreateCommand.CategoryId.Value);

            if (category == null)
            {
                return Result<GymProduct>.BadRequest("Category id is not exist");
            }
        }
        var gym = await _unitOfWork.Repository<GymProduct>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
        {
            if (gym == null)
            {
                return Result<GymProduct>.BadRequest("Category id is not exist");
            }

            _mapper.Map(request.CreateCommand, gym);

            await _unitOfWork.Repository<GymProduct>().UpdateAsync(gym);
            await _unitOfWork.Save(cancellationToken);

            return Result<GymProduct>.Success("Updated GymProduct");
        }
    }
}