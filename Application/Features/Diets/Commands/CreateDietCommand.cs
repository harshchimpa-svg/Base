using Application.Common.Mappings.Commons;
using Application.Features.Clientses.Command;
using Application.Features.Diets.Queries;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Clientses;
using Domain.Entities.DietDocuments;
using Domain.Entities.Diets;
using Domain.Entities.DietTypes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Features.Diets.Commands;

public class CreateDietCommand: IRequest<Result<string>>, ICreateMapFrom<Diet>
{
    public int? DietTypeId { get; set; }
    public   string Name { get; set; }
    public DateTime Time { get; set; }
    public string Description { get; set; }
    public List<IFormFile> Images { get; set; }
}

internal class CreateDietCommandHandler : IRequestHandler<CreateDietCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService  _fileService;
    
    public CreateDietCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,IFileService fileService)
    {
        _fileService=fileService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateDietCommand request, CancellationToken cancellationToken) 
    {
        if (request.DietTypeId.HasValue)
        {
            var houseExists = await _unitOfWork.Repository<DietType>().GetByID(request.DietTypeId.Value);

            if (houseExists == null)
            {
                return Result<string>.BadRequest("DietTypeId does not exist.");
            }
        }
        var diet = _mapper.Map<Diet>(request);

        await _unitOfWork.Repository<Diet>().AddAsync(diet);
        await _unitOfWork.Save(cancellationToken);

        foreach (var image in request.Images)
        {
            var url = await _fileService.UploadAsync(image,"DietImages");

            var dietImage = new DietDocument()
            {
                Document = url,
                DietId = diet.Id,
            };
            
            await _unitOfWork.Repository<DietDocument>().AddAsync(dietImage);
        }
        
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Diet created successfully.");
    }
}