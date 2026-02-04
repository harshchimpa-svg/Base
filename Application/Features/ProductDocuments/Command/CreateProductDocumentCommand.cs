
using Application.Common.Mappings.Commons;
using Application.Features.GymDocuments.Command;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.GymDocuments;
using Domain.Entities.GymProducts;
using Domain.Entities.Gyms;
using Domain.Entities.ProductDocuments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.ProductDocuments.Command;

public class CreateProductDocumentCommand : IRequest<Result<string>>, ICreateMapFrom<ProductDocument>
{
    public IFormFile ImageUrl { get; set; }
    public int? GymProductId { get; set; }
}
internal class CreateProductDocumentCommandHandler : IRequestHandler<CreateProductDocumentCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CreateProductDocumentCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateProductDocumentCommand request, CancellationToken cancellationToken)
    {
        if (request.GymProductId.HasValue)
        {
            var gymExists = await _unitOfWork.Repository<GymProduct>().GetByID(request.GymProductId.Value);

            if (gymExists == null)
            {
                return Result<string>.BadRequest("Product Id is not exit");
            }
        }
        var imageUrl = await _fileService.UploadAsync(request.ImageUrl, "ProductDocument");

        var document = new ProductDocument
        {
            ImageUrl = imageUrl,
        };
        var productDocument = _mapper.Map<ProductDocument>(request);

        await _unitOfWork.Repository<ProductDocument>().AddAsync(productDocument);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Product document Created");
    }
}