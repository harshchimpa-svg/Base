using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Vendors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Vendors.Command;

public class UpdateVendorCommand : IRequest<Result<Vendor>>
{
    public int Id { get; set; }
    public CreateVendorCommand CreateCommand { get; set; } = new();

    public UpdateVendorCommand(int id, CreateVendorCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}

internal class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Result<Vendor>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public UpdateVendorCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IFileService fileService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }
    
    public async Task<Result<Vendor>> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)

    {
        if (request.CreateCommand.Profile == null || request.CreateCommand.Profile.Length == 0)
        {
            return Result<Vendor>.BadRequest("Image is required.");
        }

        var Vendor = await _unitOfWork.Repository<Vendor>()
            .Entities
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (Vendor == null)
            return Result<Vendor>.BadRequest("Vendor not found.");

        _mapper.Map(request.CreateCommand, Vendor);
        
        Vendor.Profile = await _fileService.UploadAsync(request.CreateCommand.Profile, "documents");

        await _unitOfWork.Repository<Vendor>().UpdateAsync(Vendor);
        await _unitOfWork.Save(cancellationToken);

        return Result<Vendor>.Success(Vendor, "Vendor updated successfully.");
    }
}