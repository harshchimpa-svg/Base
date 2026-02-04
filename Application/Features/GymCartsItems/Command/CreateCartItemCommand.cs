

using Application.Common.Mappings.Commons;
using Application.Features.Gyms.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymCartItem;
using Domain.Entities.GymProducts;
using Domain.Entities.Gyms;
using MediatR;
using Shared;

namespace Application.Features.GymCartsItems.Command;

public class CreateCartItemCommand : IRequest<Result<string>>,ICreateMapFrom<CartItem>
{
    public int Quantity { get; set; }
    public int? GymProductId { get; set; }
}
internal class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        if (request.GymProductId.HasValue)
        {
            var productExists = await _unitOfWork.Repository<GymProduct>().GetByID(request.GymProductId.Value);
            if (productExists == null)
            {
                return Result<string>.BadRequest("Product id not exit");
            }
        }

        var gym = _mapper.Map<CartItem>(request);

        await _unitOfWork.Repository<CartItem>().AddAsync(gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("CartItem Created Successfully");

    }
}
