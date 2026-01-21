using Application.Common.Mappings.Commons;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Abouts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Abouts.Commands;

public class CreateAboutCommand: IRequest<Result<string>>, ICreateMapFrom<About>
{
    public string Name { get; set; }
    public IFormFile Profile { get; set; }
    public string SubTitel { get; set; }
}

internal class CreateAboutCommandHandler: IRequestHandler<CreateAboutCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CreateAboutCommandHandler(IMapper mapper,IFileService fileService, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<string>> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await _fileService.UploadAsync(request.Profile, "About");

        var document = new About
        {
            Profile = imageUrl,
        };
        
        var About = _mapper.Map<About>(request);

        await _unitOfWork.Repository<About>().AddAsync(About);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("About created successfully.");
    }
}