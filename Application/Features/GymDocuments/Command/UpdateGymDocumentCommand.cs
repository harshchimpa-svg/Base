using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymDocuments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.GymDocuments.Command;

public class UpdateGymDocumentCommand : IRequest<Result<GymDocument>>
{

    public int Id { get; set; }
    public CreateGymDocumentCommand CreateCommand { get; set; } = new();

    public UpdateGymDocumentCommand(int id, CreateGymDocumentCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
public record GetGymDocumentDto(IFormFile File);

internal class UpdateGymDocumentCommandHandler : IRequestHandler<UpdateGymDocumentCommand, Result<GymDocument>>
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGymDocumentCommandHandler(IMapper mapper, IUnitOfWork CategoryRepository, IFileService fileService)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = CategoryRepository;
    }

    public async Task<Result<GymDocument>> Handle(UpdateGymDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.GymId.HasValue)
        {
            var gym = await _unitOfWork.Repository<GymDocument>().GetByID(request.CreateCommand.GymId.Value);

            if (gym == null)
            {
                return Result<GymDocument>.BadRequest("Gym Id is not exist.");
            }
        }
        if (request.CreateCommand == null || request.CreateCommand.ImageUrl.Length == 0)
            return Result<GymDocument>.BadRequest("Image is required.");

        var Gym = await _unitOfWork.Repository<GymDocument>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Gym == null)
        {
            return Result<GymDocument>.BadRequest("Sorry id not found");
            Gym.ImageUrl = await _fileService.UploadAsync(request.CreateCommand.ImageUrl, "documents");
        }

        _mapper.Map(request.CreateCommand, Gym);

        await _unitOfWork.Repository<GymDocument>().UpdateAsync(Gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<GymDocument>.Success("Update Category...");
    }
}
