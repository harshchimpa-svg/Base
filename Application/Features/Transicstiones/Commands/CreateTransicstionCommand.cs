using Application.Common.Mappings.Commons;
using Application.Features.Catgoryes.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.CatagoryTypes;
using Domain.Entities.Catagoryes;
using Domain.Entities.Transicstions;
using MediatR;
using Shared;

namespace Application.Features.Transicstiones.Commands;

public class CreateTransicstionCommand : IRequest<Result<string>>, ICreateMapFrom<Transicstion>
{
    public int? CatgoryId { get; set; }
    public string Comments { get; set; }
    public string paticular { get; set; }
    public int Amount { get; set; }
    public int? PaymentHeadId { get; set; }

}
internal class CreateTransicstionCommandHandler : IRequestHandler<CreateTransicstionCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTransicstionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateTransicstionCommand request, CancellationToken cancellationToken)
    {
        if (request.CatgoryId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<Transicstion>().GetByID(request.CatgoryId.Value);

            if (parentExists == null)
            {
                return Result<string>.BadRequest("Parent Id is not exist.");
            }
        }

            var Catgory = _mapper.Map<Transicstion>(request);

            await _unitOfWork.Repository<Transicstion>().AddAsync(Catgory);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Transicstion created successfully.");
    }
    
}