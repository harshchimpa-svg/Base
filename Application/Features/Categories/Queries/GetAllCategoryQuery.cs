using Application.Dto.Categoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Categories.Queries;

public class GetAllCategoryQuery : IRequest<PaginatedResult<GetCategoriesDto>>
{
    public int? ParentId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, PaginatedResult<GetCategoriesDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PaginatedResult<GetCategoriesDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var queryable = _unitOfWork.Repository<Category>().Entities.AsQueryable();

        if (request.ParentId.HasValue)
        {
            queryable = queryable.Where(x => x.ParentId == request.ParentId);
        }

        int count = await queryable.CountAsync();

        if (request.PageNumber != 0 && request.PageSize != 0)
        {
            queryable = queryable
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }
        var query = await queryable.ToListAsync();

        var map = _mapper.Map<List<GetCategoriesDto>>(query);

        return PaginatedResult<GetCategoriesDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
