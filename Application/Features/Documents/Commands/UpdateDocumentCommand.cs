using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Documents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Documents.Commands;

public class UpdateDocumentCommand : IRequest<Result<Document>>
{
    public int Id { get; set; }
    public UpdateDocumentDto Image { get; set; }
    //public CreateDocumentCommand CreateCommand { get; set; } = new();

    public UpdateDocumentCommand(int id, UpdateDocumentDto image)
    {
        Id = id;
        Image = image;
    }
}

public record UpdateDocumentDto(IFormFile File);

internal class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, Result<Document>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public UpdateDocumentCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Result<Document>> Handle(
        UpdateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Image == null || request.Image.File.Length == 0)
            return Result<Document>.BadRequest("Image is required.");

        var document = await _unitOfWork.Repository<Document>()
            .GetByID(request.Id);

        if (document == null)
            return Result<Document>.BadRequest("Document not found.");

        // Delete old image
        //if (!string.IsNullOrEmpty(document.ImageUrl))
        //{
        //    await _fileService..DeleteAsync(document.ImageUrl);
        //}

        // Upload new image
        document.ImageUrl = await _fileService.UploadAsync(
            request.Image.File,
            "documents");

        await _unitOfWork.Save(cancellationToken);

        return Result<Document>.Success(document, "Document updated successfully.");
    }
}