using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagoryes;
using Domain.Entities.TranstionDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.TranstionDocuments.Command;

public class UpdateTranstionDocumentCommand : IRequest<Result<TranstionDocument>>
{

    public int Id { get; set; }
    public CreateTranstionDocumentCommand CreateCommand { get; set; } = new();

    public UpdateTranstionDocumentCommand(int id, CreateTranstionDocumentCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateTranstionDocumentCommandHandler : IRequestHandler<UpdateTranstionDocumentCommand, Result<TranstionDocument>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTranstionDocumentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TranstionDocument>> Handle(UpdateTranstionDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.CatgoryId.HasValue)
        {
            var parent = await _unitOfWork.Repository<TranstionDocument>().GetByID(request.CreateCommand.CatgoryId.Value);

            if (parent == null)
            {
                return Result<TranstionDocument>.BadRequest("CatgoryId is not exist.");
            }
        }

        var Chair = await _unitOfWork.Repository<TranstionDocument>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Chair == null)
        {
            return Result<TranstionDocument>.BadRequest("Chair id not found");
        }

        _mapper.Map(request.CreateCommand, Chair);

        await _unitOfWork.Repository<TranstionDocument>().UpdateAsync(Chair);
        await _unitOfWork.Save(cancellationToken);

        return Result<TranstionDocument>.Success("Update Chair...");
    }
}