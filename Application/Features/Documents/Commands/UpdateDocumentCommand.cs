using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Documents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Documents.Commands;

public class UpdateDocumentCommand : IRequest<Result<Document>>
{
    public int Id { get; set; }
    public IFormFile Image { get; set; }

    public UpdateDocumentCommand(int id, IFormFile image)
    {
        Id = id;
        Image = image;
    }
}

internal class UpdateDocumentCommandHandler: IRequestHandler<UpdateDocumentCommand, Result<Document>>
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
        var document = await _unitOfWork.Repository<Document>()
            .GetByID(request.Id);

        if (document == null)
            return Result<Document>.BadRequest("Document not found.");

        document.ImageUrl = await _fileService.UploadAsync(
            request.Image,
            "documents");

        await _unitOfWork.Repository<Document>().UpdateAsync(document);
        await _unitOfWork.Save(cancellationToken);

        return Result<Document>.Success("Document updated successfully.");
    }
}
