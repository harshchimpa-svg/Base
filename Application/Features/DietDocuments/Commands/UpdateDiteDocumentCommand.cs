using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.DietDocuments.Commands;

public class UpdateDiteDocumentCommand: IRequest<Result<DietDocument>>
{

    public int Id { get; set; }
    public CreateDiteDocumentCommand CreateCommand { get; set; } = new();

    public UpdateDiteDocumentCommand(int id, CreateDiteDocumentCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateDiteDocumentCommandHandler : IRequestHandler<UpdateDiteDocumentCommand, Result<DietDocument>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiteDocumentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DietDocument>> Handle(UpdateDiteDocumentCommand request, CancellationToken cancellationToken)
    {

        var Service = await _unitOfWork.Repository<DietDocument>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Service == null)
        {
            return Result<DietDocument>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Service);

        await _unitOfWork.Repository<DietDocument>().UpdateAsync(Service);
        await _unitOfWork.Save(cancellationToken);

        return Result<DietDocument>.Success("Update Service...");
    }
}