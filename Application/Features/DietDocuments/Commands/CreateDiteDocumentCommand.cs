using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietDocuments;
using MediatR;
using Shared;

namespace Application.Features.DietDocuments.Commands;

public class CreateDiteDocumentCommand: IRequest<Result<string>>, ICreateMapFrom<DietDocument>
{
    public int? DietId { get; set; }
    public string Document { get; set; }
}

internal class CreateDiteDocumentCommandHandler : IRequestHandler<CreateDiteDocumentCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateDiteDocumentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {   
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateDiteDocumentCommand request, CancellationToken cancellationToken)
    {
        var Customer = _mapper.Map<DietDocument>(request);

        await _unitOfWork.Repository<DietDocument>().AddAsync(Customer);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Customer created successfully.");
    }
}