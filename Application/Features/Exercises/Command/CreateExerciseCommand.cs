using Application.Common.Mappings.Commons;
using Application.Features.Gyms.Command;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.DietDocuments;
using Domain.Entities.DietTypes;
using Domain.Entities.ExerciseDocuments;
using Domain.Entities.Exercises;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Exercises.Command;

public class CreateExerciseCommand: IRequest<Result<string>>, ICreateMapFrom<Exercise>
{
    public int? DietTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<IFormFile> Images { get; set; }

}

internal class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService  _fileService;

    public CreateExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,IFileService fileService)
    {
        _fileService=fileService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        if (request.DietTypeId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<DietType>().GetByID(request.DietTypeId.Value);
            if (parentExists == null)
            {
                return Result<string>.BadRequest("Exercise id not exit");
            }
        }

        var Exercise = _mapper.Map<Exercise>(request);

        await _unitOfWork.Repository<Exercise>().AddAsync(Exercise);
        await _unitOfWork.Save(cancellationToken);
        
        foreach (var image in request.Images)
        {
            var url = await _fileService.UploadAsync(image,"DietImages");

            var dietImage = new ExerciseDocument()
            {
                Document = url,
                ExerciseId = Exercise.Id,
            };
            
            await _unitOfWork.Repository<ExerciseDocument>().AddAsync(dietImage);
        }
        
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Exercise Created Successfully");
        
    }
}