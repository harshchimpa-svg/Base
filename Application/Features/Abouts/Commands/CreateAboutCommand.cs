using Application.Common.Mappings.Commons;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Abouts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Abouts.Commands;

public class CreateAboutCommand : IRequest<Result<string>>, ICreateMapFrom<About>
{
    public string Name { get; set; }
    public IFormFile Profile { get; set; }
    public string SubTitel { get; set; }
}

internal class CreateAboutCommandHandler : IRequestHandler<CreateAboutCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public CreateAboutCommandHandler(IFileService fileService, IUnitOfWork unitOfWork)
    {
        _fileService = fileService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        if (request.Profile == null)
            return Result<string>.BadRequest("Profile image is required");

        var imageUrl = await _fileService.UploadAsync(request.Profile, "About");

        var about = new About
        {
            Name = request.Name,
            SubTitel = request.SubTitel,
            Profile = imageUrl,
            IsActive = true
        };

        await _unitOfWork.Repository<About>().AddAsync(about);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("About created successfully");
    }
}
