using Application.Common.Mappings.Commons;
using Application.Features.DietDocuments.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietDocuments;
using Domain.Entities.ExerciseDocuments;
using Domain.Entities.Exercises;
using MediatR;
using Shared;

namespace Application.Features.ExerciseDocuments.Commands;

public class CreateExerciseDocumentCommand: IRequest<Result<string>>, ICreateMapFrom<ExerciseDocument>
{
    public int? ExerciseId { get; set; }
    public string Document { get; set; }
}

internal class CreateExerciseDocumentCommandHandler : IRequestHandler<CreateExerciseDocumentCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateExerciseDocumentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {   
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateExerciseDocumentCommand request, CancellationToken cancellationToken)
    {
        
        if (request.ExerciseId.HasValue)
        {
            var houseExists = await _unitOfWork.Repository<Exercise>().GetByID(request.ExerciseId.Value);

            if (houseExists == null)
            {
                return Result<string>.BadRequest("ExerciseId does not exist.");
            }
        }
        var ExerciseDocument = _mapper.Map<ExerciseDocument>(request);

        await _unitOfWork.Repository<ExerciseDocument>().AddAsync(ExerciseDocument);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("ExerciseDocument created successfully.");
    }
}