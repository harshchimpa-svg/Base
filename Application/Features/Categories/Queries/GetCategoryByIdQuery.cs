using Application.Dto.Categoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Categories;
using MediatR;
using Shared;

namespace Application.Features.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<Result<GetCategoryDto>>
{
    public int Id { get; set; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Repository<Category>().GetByID(request.Id);

        if (category == null)
        {
            return Result<GetCategoryDto>.BadRequest("Category not found.");
        }

        var mapData = _mapper.Map<GetCategoryDto>(category);

        return Result<GetCategoryDto>.Success(mapData, "Category");
    }
}