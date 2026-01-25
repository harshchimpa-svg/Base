using Application.Dto.Categoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagories;
using MediatR;
using Shared;

namespace Application.Features.Categoryes.Queries;

public class GetCategoriByIdQuery : IRequest<Result<GetCategoryDto>>
{
    public int Id { get; set; }

    public GetCategoriByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetCategoryesByIdQueryHandler : IRequestHandler<GetCategoriByIdQuery, Result<GetCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryesByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCategoryDto>> Handle(GetCategoriByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _unitOfWork.Repository<Category>().GetByID(request.Id);

        if (location == null)
        {
            return Result<GetCategoryDto>.BadRequest("Catgory not found.");
        }

        var mapData = _mapper.Map<GetCategoryDto>(location);

        return Result<GetCategoryDto>.Success(mapData, "Catgory");
    }
}