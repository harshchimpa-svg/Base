using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Categoryes.Command;

public class UpdateCategoriCommand : IRequest<Result<Category>>
{

    public int Id { get; set; } 
    public CreateCategoriCommand CreateCommand { get; set; } = new();

    public UpdateCategoriCommand(int id, CreateCategoriCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
public record GetCategoriDto(IFormFile File);

internal class UpdateCategoriCommandHandler : IRequestHandler<UpdateCategoriCommand, Result<Category>>
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoriCommandHandler(IMapper mapper, IUnitOfWork CategoriRepository, IFileService fileService)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = CategoriRepository;
    }
    
    public async Task<Result<Category>> Handle(UpdateCategoriCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.ParentId.HasValue)                  
        {
            var parent = await _unitOfWork.Repository<Category>().GetByID(request.CreateCommand.ParentId.Value);

            if (parent == null)
            {
                return Result<Category>.BadRequest("Parent Id is not exist.");
            }
        }
        if (request.CreateCommand == null || request.CreateCommand.ImageUrl.Length == 0)
            return Result<Category>.BadRequest("Image is required.");
         
        var Categori = await _unitOfWork.Repository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Categori == null)
        {
            return Result<Category>.BadRequest("Sorry id not found");
            Categori.ImageUrl = await _fileService.UploadAsync(request.CreateCommand.ImageUrl, "documents");
        }

        _mapper.Map(request.CreateCommand, Categori);

        await _unitOfWork.Repository<Category>().UpdateAsync(Categori);
        await _unitOfWork.Save(cancellationToken);

        return Result<Category>.Success("Update Categori...");
    }
}
