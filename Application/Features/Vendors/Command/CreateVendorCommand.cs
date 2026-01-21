using Application.Common.Mappings.Commons;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Vendors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Vendors.Command;

public class CreateVendorCommand : IRequest<Result<Vendor>>, IMapFrom<Vendor>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile Profile { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
}

internal class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, Result<Vendor>>
{
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVendorCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<Result<Vendor>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        if (request.Profile == null || request.Profile.Length == 0)
            return Result<Vendor>.BadRequest("Image is required.");

        var imageUrl = await _fileService.UploadAsync(request.Profile, "vendors");

        var vendor = new Vendor
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            Profile = imageUrl,
            Website = request.Website
        };

        await _unitOfWork.Repository<Vendor>().AddAsync(vendor);
        await _unitOfWork.Save(cancellationToken);

        return Result<Vendor>.Success(vendor, "Vendor created successfully.");
    }
}
