

using Application.Features.GymDocuments.Command;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymDocuments;
using Domain.Entities.ProductDocuments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ProductDocuments.Command;

public class UpdateProductDocumentCommand : IRequest<Result<ProductDocument>>
{
    public int Id { get; set; }
    public CreateProductDocumentCommand CreateCommand { get; set; } = new();

    public UpdateProductDocumentCommand(int id, CreateProductDocumentCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
public record GetGymDocumentDto(IFormFile File);

internal class UpdateProductDocumentCommandHandler : IRequestHandler<UpdateProductDocumentCommand, Result<ProductDocument>>
{
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductDocumentCommandHandler(IMapper mapper, IUnitOfWork CategoryRepository, IFileService fileService)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = CategoryRepository;
    }

    public async Task<Result<ProductDocument>> Handle(UpdateProductDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.GymProductId.HasValue)
        {
            var gym = await _unitOfWork.Repository<ProductDocument>().GetByID(request.CreateCommand.GymProductId.Value);

            if (gym == null)
            {
                return Result<ProductDocument>.BadRequest("Produt  Id is not exist.");
            }
        }
        if (request.CreateCommand == null || request.CreateCommand.ImageUrl.Length == 0)
            return Result<ProductDocument>.BadRequest("Image is required.");

        var Gym = await _unitOfWork.Repository<ProductDocument>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Gym == null)
        {
            return Result<ProductDocument>.BadRequest("Sorry id not found");
            Gym.ImageUrl = await _fileService.UploadAsync(request.CreateCommand.ImageUrl, "documents");
        }

        _mapper.Map(request.CreateCommand, Gym);

        await _unitOfWork.Repository<ProductDocument>().UpdateAsync(Gym);
        await _unitOfWork.Save(cancellationToken);

        return Result<ProductDocument>.Success("Update Category...");
    }
}

