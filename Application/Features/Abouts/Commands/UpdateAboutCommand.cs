using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Abouts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Abouts.Command;

public class UpdateAboutCommand: IRequest<Result<About>>
{

    public int Id { get; set; } 
    public CreateAboutCommand CreateCommand { get; set; } = new();

    public UpdateAboutCommand(int id, CreateAboutCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
public record GetAboutDto(IFormFile File);

internal class UpdateAboutCommandHandler : IRequestHandler<UpdateAboutCommand, Result<About>>
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAboutCommandHandler(IMapper mapper, IUnitOfWork UnitOfWork, IFileService fileService)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = UnitOfWork;
    }

    public async Task<Result<About>> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)   
    {
        if (request.CreateCommand == null || request.CreateCommand.Profile.Length == 0)
            return Result<About>.BadRequest("Image is required.");
         
        var about = await _unitOfWork.Repository<About>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);  

        if (about == null)
        {
            return Result<About>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, about);

        await _unitOfWork.Repository<About>().UpdateAsync(about);
        await _unitOfWork.Save(cancellationToken);

        return Result<About>.Success("Update About...");
    }
}