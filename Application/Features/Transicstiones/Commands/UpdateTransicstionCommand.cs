using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Transicstions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Transicstiones.Commands;

public class UpdateTransicstionCommand : IRequest<Result<Transicstion>>
{

    public int Id { get; set; }
    public CreateTransicstionCommand CreateCommand { get; set; } = new();

    public UpdateTransicstionCommand(int id, CreateTransicstionCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateTransicstionCommandHandler : IRequestHandler<UpdateTransicstionCommand, Result<Transicstion>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransicstionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Transicstion>> Handle(UpdateTransicstionCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.CatgoryId.HasValue)
        {
            var parent = await _unitOfWork.Repository<Transicstion>().GetByID(request.CreateCommand.CatgoryId.Value);

            if (parent == null)
            {
                return Result<Transicstion>.BadRequest("Parent Id is not exist.");
            }
        }

        var Transicstion = await _unitOfWork.Repository<Transicstion>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Transicstion == null)
        {
            return Result<Transicstion>.BadRequest("Transicstion id not found");
        }

        _mapper.Map(request.CreateCommand, Transicstion);

        await _unitOfWork.Repository<Transicstion>().UpdateAsync(Transicstion);
        await _unitOfWork.Save(cancellationToken);

        return Result<Transicstion>.Success("Update Transicstion...");
    }
}