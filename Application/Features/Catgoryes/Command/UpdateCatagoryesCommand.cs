using Application.Features.Roles.Command;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagoryes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Catgoryes.Command;

public class UpdateCatagoryesCommand : IRequest<Result<Catgory>>
{

    public int Id { get; set; }
    public CreateCatagoryesCommand CreateCommand { get; set; } = new();

    public UpdateCatagoryesCommand(int id, CreateCatagoryesCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateCatagoryesCommandHandler : IRequestHandler<UpdateCatagoryesCommand, Result<Catgory>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCatagoryesCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Catgory>> Handle(UpdateCatagoryesCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.ParentId.HasValue)
        {
            var parent = await _unitOfWork.Repository<Catgory>().GetByID(request.CreateCommand.ParentId.Value);

            if (parent == null)
            {
                return Result<Catgory>.BadRequest("Parent Id is not exist.");
            }
        }

        var location = await _unitOfWork.Repository<Catgory>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (location == null)
        {
            return Result<Catgory>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, location);

        await _unitOfWork.Repository<Catgory>().UpdateAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<Catgory>.Success("Update location...");
    }
}