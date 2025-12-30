using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Documents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Documents.Commands;


public interface IFileService
{
    Task<string> UploadAsync(IFormFile file, string folderName);
}

public class CreateDocumentCommand
    : IRequest<Result<string>>, ICreateMapFrom<Document>
{
    public IFormFile Image { get; set; }
}

internal class CreateDocumentCommandHandler
    : IRequestHandler<CreateDocumentCommand, Result<string>>
{
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDocumentCommandHandler(
        IFileService fileService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(
        CreateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var imageUrl = await _fileService.UploadAsync(
            request.Image,
            "documents");

        var document = new Document
        {
            ImageUrl = imageUrl
        };

        await _unitOfWork.Repository<Document>().AddAsync(document);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Document created successfully.");
    }
}
