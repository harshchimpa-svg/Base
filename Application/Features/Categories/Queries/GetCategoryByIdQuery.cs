using Application.Dto.Categoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagories;
using MediatR;
using Shared;

namespace Application.Features.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<Result<GetCategoriesDto>>
{
    public int Id { get; set; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoriesDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCategoriesDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Repository<Category>().GetByID(request.Id);

        if (category == null)
        {
            return Result<GetCategoriesDto>.BadRequest("Catgory not found.");
        }

        var mapData = _mapper.Map<GetCategoriesDto>(category);

        return Result<GetCategoriesDto>.Success(mapData, "Catgory");
    }
}