using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DietDocuments.Commands;

public class UpdateDietDocumentCommand: IRequest<Result<DietDocument>>
{

    public int Id { get; set; }
    public CreateDietDocumentCommand CreateCommand { get; set; } = new();

    public UpdateDietDocumentCommand(int id, CreateDietDocumentCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateDietDocumentCommandHandler : IRequestHandler<UpdateDietDocumentCommand, Result<DietDocument>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDietDocumentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DietDocument>> Handle(UpdateDietDocumentCommand request, CancellationToken cancellationToken)
    {

        var dietdocument = await _unitOfWork.Repository<DietDocument>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (dietdocument == null)
        {
            return Result<DietDocument>.BadRequest("DietDocument id not found");
        }

        _mapper.Map(request.CreateCommand, dietdocument);

        await _unitOfWork.Repository<DietDocument>().UpdateAsync(dietdocument);
        await _unitOfWork.Save(cancellationToken);

        return Result<DietDocument>.Success("Update DietDocuments...");
    }
}