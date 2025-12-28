using Application.Dto.Catgoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagoryes;
using MediatR;
using Shared;

namespace Application.Features.Catgoryes.Queries;

public class GetCatgoryesByIdQuery : IRequest<Result<GetCatgoryDto>>
{
    public int Id { get; set; }

    public GetCatgoryesByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetCatgoryesByIdQueryHandler : IRequestHandler<GetCatgoryesByIdQuery, Result<GetCatgoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCatgoryesByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCatgoryDto>> Handle(GetCatgoryesByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Catgory>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetCatgoryDto>.BadRequest("Location not found.");
        }

        var mapData = _mapper.Map<GetCatgoryDto>(location);

        return Result<GetCatgoryDto>.Success(mapData, "Location");
    }
}