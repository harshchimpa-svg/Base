

using Application.Common.Mappings.Commons;
using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCategorys;
using Domain.Entities.GymProducts;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.GymProducts.Command;

public class CreateGymProductCommand : IRequest<Result<string>>,ICreateMapFrom<GymProduct>
{
    public int Tax {  get; set; }
    public decimal Price { get; set; }
    public int? CategoryId { get; set; }
}
internal class CreateGymProductCommandHandler : IRequestHandler<CreateGymProductCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGymProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateGymProductCommand request, CancellationToken cancellationToken)
    {
        if (request.CategoryId.HasValue)
        {
            var categoryExists = await _unitOfWork.Repository<GymCategory>().GetByID(request.CategoryId.Value);
            if (categoryExists == null)
            {
                return Result<string>.BadRequest("Category id not exit");
            }
        }

        var gym = _mapper.Map<GymProduct>(request);

        await _unitOfWork.Repository<GymProduct>().AddAsync(gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Product Created Successfully");

    }
}