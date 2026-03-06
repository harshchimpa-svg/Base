
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
internal class UpdateGymProductCommandHandler : IRequestHandler<UpdateGymProductCommand, Result<GymProduct>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGymProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GymProduct>> Handle(UpdateGymProductCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.CategoryId.HasValue)
        {
            var gymProducts = await _unitOfWork.Repository<GymProduct>().GetByID(request.CreateCommand.CategoryId.Value);

            if (gymProducts == null)
            {
                return Result<GymProduct>.BadRequest("CartItem does not exist");
            }
        }
        var gymProduct = await _unitOfWork.Repository<GymProduct>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);
        {
            if (gymProduct == null)
            {
                return Result<GymProduct>.BadRequest("GymProduct id is not exist");
            }

            _mapper.Map(request.CreateCommand, gymProduct);

            await _unitOfWork.Repository<GymProduct>().UpdateAsync(gymProduct);
            await _unitOfWork.Save(cancellationToken);

            return Result<GymProduct>.Success("Updated GymProduct");
        }
    }
}