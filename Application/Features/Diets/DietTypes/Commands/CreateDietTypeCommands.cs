using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietTypes;
using MediatR;
using Shared;

namespace Application.Features.DietTypes.Command;

public class CreateDietTypeCommand: IRequest<Result<string>>, ICreateMapFrom<DietType>
{
    public string? Name { get; set; }
}
internal class CreateDietTypeCommandHandler : IRequestHandler<CreateDietTypeCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDietTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateDietTypeCommand request, CancellationToken cancellationToken)
    {
        var dietTypes = _mapper.Map<DietType>(request);

        await _unitOfWork.Repository<DietType>().AddAsync(dietTypes);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("DietTypes Created Successfully");
        
    }
}