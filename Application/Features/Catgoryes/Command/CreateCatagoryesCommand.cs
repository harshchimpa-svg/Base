using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Common.Enums.CatagoryTypes;
using Domain.Entities.Catagoryes;
using MediatR;
using Shared;

namespace Application.Features.Catgoryes.Command;

public class CreateCatagoryesCommand : IRequest<Result<string>>, ICreateMapFrom<Catgory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public int? ParentId { get; set; }
    public CatgoryType CatgoryType { get; set; }
}
internal class CreateCatagoryesCommandHandler : IRequestHandler<CreateCatagoryesCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCatagoryesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateCatagoryesCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<Catgory>().GetByID(request.ParentId.Value);

            if (parentExists == null)
            {
                return Result<string>.BadRequest("Parent Id is not exist.");
            }
        }

        var Catgory = _mapper.Map<Catgory>(request);

        await _unitOfWork.Repository<Catgory>().AddAsync(Catgory);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Catgory created successfully.");
    }
}