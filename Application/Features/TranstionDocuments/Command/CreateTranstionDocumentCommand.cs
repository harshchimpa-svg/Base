using Application.Common.Mappings.Commons;
using Application.Features.Catgoryes.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagoryes;
using Domain.Entities.TranstionDocuments;
using MediatR;
using Shared;

namespace Application.Features.TranstionDocuments.Command;

public class CreateTranstionDocumentCommand : IRequest<Result<string>>, ICreateMapFrom<TranstionDocument>
{
    public int? CatgoryId { get; set; }
    public int TransicstionId { get; set; }

}
internal class CreateTranstionDocumentCommandHandler : IRequestHandler<CreateTranstionDocumentCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTranstionDocumentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateTranstionDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.CatgoryId.HasValue)
        {
            var houseExists = await _unitOfWork.Repository<Catgory>().GetByID(request.CatgoryId.Value);

            if (houseExists == null)
            {
                return Result<string>.BadRequest("Catgory Id does not exist.");
            }
        }

        var House = _mapper.Map<TranstionDocument>(request);

        await _unitOfWork.Repository<TranstionDocument>().AddAsync(House);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("TranstionDocument created successfully.");
    }
}