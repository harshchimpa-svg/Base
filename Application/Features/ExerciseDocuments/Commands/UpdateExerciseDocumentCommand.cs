using Application.Features.ExerciseDocuments.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ExerciseDocuments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ExerciseDocuments.Command;

public class UpdateExerciseDocumentCommand: IRequest<Result<ExerciseDocument>>
{

    public int Id { get; set; }
    public CreateExerciseDocumentCommand CreateCommand { get; set; } = new();

    public UpdateExerciseDocumentCommand(int id, CreateExerciseDocumentCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateExerciseDocumentCommandHandler : IRequestHandler<UpdateExerciseDocumentCommand, Result<ExerciseDocument>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateExerciseDocumentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ExerciseDocument>> Handle(UpdateExerciseDocumentCommand request, CancellationToken cancellationToken)
    {

        var exerciseDocument = await _unitOfWork.Repository<ExerciseDocument>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (exerciseDocument == null)
        {
            return Result<ExerciseDocument>.BadRequest("ExerciseDocument id not found");
        }

        _mapper.Map(request.CreateCommand, exerciseDocument);

        await _unitOfWork.Repository<ExerciseDocument>().UpdateAsync(exerciseDocument);
        await _unitOfWork.Save(cancellationToken);

        return Result<ExerciseDocument>.Success("Update ExerciseDocument...");
    }
}