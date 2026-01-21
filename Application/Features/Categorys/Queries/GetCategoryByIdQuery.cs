using Application.Dto.Catgoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagoryes;
using MediatR;
using Shared;

namespace Application.Features.Catgoryes.Queries;

public class GetCategoryByIdQuery : IRequest<Result<GetCategoryDto>>
{
    public int Id { get; set; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetCatgoryesByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCatgoryesByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
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