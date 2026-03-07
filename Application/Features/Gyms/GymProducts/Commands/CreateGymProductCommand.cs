using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCategories;
using Domain.Entities.GymProducts;
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
            var gymProducts = await _unitOfWork.Repository<GymCategoryes>().GetByID(request.CategoryId.Value);
            if (gymProducts == null)
            {
                return Result<string>.BadRequest("GymProduct id not exit");
            }
        }

        var gymProduct = _mapper.Map<GymProduct>(request);

        await _unitOfWork.Repository<GymProduct>().AddAsync(gymProduct);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Product Created Successfully");

    }
}