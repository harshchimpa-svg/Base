using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietDocuments;
using MediatR;
using Shared;

namespace Application.Features.DietDocuments.Commands;

public class CreateDietDocumentCommand: IRequest<Result<string>>, ICreateMapFrom<DietDocument>
{
    public int DietId { get; set; }
    public string Document { get; set; }
}

internal class CreateDiteDocumentCommandHandler : IRequestHandler<CreateDietDocumentCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateDiteDocumentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {   
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateDietDocumentCommand request, CancellationToken cancellationToken)
    {
        var dietDocument = _mapper.Map<DietDocument>(request);

        await _unitOfWork.Repository<DietDocument>().AddAsync(dietDocument);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("DietDocument created successfully.");
    }
}