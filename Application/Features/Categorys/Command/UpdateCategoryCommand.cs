using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagoryes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Catgoryes.Command;

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
public record GetCategoryDto(IFormFile File);

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Category>>
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork CategoryRepository, IFileService fileService)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = CategoryRepository;
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
         
        var Category = await _unitOfWork.Repository<Category>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Category == null)
        {
            return Result<Category>.BadRequest("Sorry id not found");
            Category.ImageUrl = await _fileService.UploadAsync(request.CreateCommand.ImageUrl, "documents");
        }

        _mapper.Map(request.CreateCommand, Category);

        await _unitOfWork.Repository<Category>().UpdateAsync(Category);
        await _unitOfWork.Save(cancellationToken);

        return Result<Category>.Success("Update Category...");
    }
}
