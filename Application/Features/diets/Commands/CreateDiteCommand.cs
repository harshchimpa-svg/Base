using Application.Common.Mappings.Commons;
using Application.Features.Clientses.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using Domain.Entities.Dites;
using Domain.Entities.DiteTypes;
using MediatR;
using Shared;

namespace Application.Features.Dites.Commands;

public class CreateDiteCommand: IRequest<Result<string>>, ICreateMapFrom<diet>
{
    public int? DiteTypeId { get; set; }
    public   string Name { get; set; }
    public DateTime Time { get; set; }
    public string Description { get; set; }
}

internal class CreateDiteCommandHandler : IRequestHandler<CreateDiteCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateDiteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateDiteCommand request, CancellationToken cancellationToken) 
    {
        if (request.DiteTypeId.HasValue)
        {
            var houseExists = await _unitOfWork.Repository<DiteType>().GetByID(request.DiteTypeId.Value);

            if (houseExists == null)
            {
                return Result<string>.BadRequest("DiteTypeId does not exist.");
            }
        }
        var Dite = _mapper.Map<diet>(request);

        await _unitOfWork.Repository<diet>().AddAsync(Dite);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Dite created successfully.");
    }
}