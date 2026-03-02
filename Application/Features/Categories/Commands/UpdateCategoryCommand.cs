using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Categories.Command;

public class UpdateCategoryCommand : IRequest<Result<Category>>
{

    public int Id { get; set; } 
    public CreateCategoryCommand CreateCommand { get; set; } = new();

    public UpdateCategoryCommand(int id, CreateCategoryCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
public record GetCategoriDto(IFormFile File);

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Category>>
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork CategoriRepository, IFileService fileService)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = CategoriRepository;
    }
    
    public async Task<Result<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
         
        var category = await _unitOfWork.Repository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (category == null)
        {
            return Result<Category>.BadRequest("Sorry id not found");
            category.ImageUrl = await _fileService.UploadAsync(request.CreateCommand.ImageUrl, "documents");
        }

        _mapper.Map(request.CreateCommand, category);

        await _unitOfWork.Repository<Category>().UpdateAsync(category);
        await _unitOfWork.Save(cancellationToken);

        return Result<Category>.Success("Update Category...");
    }
}
