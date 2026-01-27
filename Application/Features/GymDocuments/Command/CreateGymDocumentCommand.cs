

using Application.Common.Mappings.Commons;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymDocuments;
using Domain.Entities.Gyms;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;


namespace Application.Features.GymDocuments.Command;

public class CreateGymDocumentCommand : IRequest<Result<string>>, ICreateMapFrom<GymDocument>
{
    public IFormFile ImageUrl { get; set; }
    public int? GymId { get; set; }
} 

internal class CreateGymDocumentCommandHandler : IRequestHandler<CreateGymDocumentCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CreateGymDocumentCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateGymDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.GymId.HasValue)
        {
            var gymExists = await _unitOfWork.Repository<Gym>().GetByID(request.GymId.Value);

            if (gymExists == null)
            {
                return Result<string>.BadRequest("Gym Id is not exit");
            }
        }
        var imageUrl = await _fileService.UploadAsync(request.ImageUrl, "GymDocument");

        var document = new GymDocument
        {
            ImageUrl = imageUrl,
        };
        var gymDocument = _mapper.Map<GymDocument>(request);

        await _unitOfWork.Repository<GymDocument>().AddAsync(gymDocument);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Gym document Created");
    }
}
