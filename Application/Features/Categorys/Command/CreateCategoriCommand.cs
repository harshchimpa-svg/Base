using Application.Common.Mappings.Commons;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Categoryes.Command;

public class CreateCategoriCommand  : IRequest<Result<string>>, ICreateMapFrom<Category>
{
    public string Name { get; set; }
    public IFormFile ImageUrl { get; set; } 
    public string Description { get; set; }
    public int? ParentId { get; set; }
}

internal class CreateCategoriCommandHandler: IRequestHandler<CreateCategoriCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CreateCategoriCommandHandler(IMapper mapper,IFileService fileService, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateCategoriCommand request,CancellationToken cancellationToken)
    {
        if (request.ParentId.HasValue)
        {
            var parentExists = await _unitOfWork.Repository<Category>().GetByID(request.ParentId.Value);

            if (parentExists == null)
            {
                return Result<string>.BadRequest("Parent Id is not exist.");
            }
        }
        var imageUrl = await _fileService.UploadAsync(request.ImageUrl, "Categori");

        var document = new Category
        {
            ImageUrl = imageUrl,
        };
        
        var Categori = _mapper.Map<Category>(request);

        await _unitOfWork.Repository<Category>().AddAsync(Categori);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Categori created successfully.");
    }
}
