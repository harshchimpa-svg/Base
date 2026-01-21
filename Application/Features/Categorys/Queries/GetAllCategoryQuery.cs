using Application.Dto.Catgoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagoryes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Catgoryes.Queries;

public class GetAllCategoryQuery : IRequest<PaginatedResult<GetCategoryDto>>
{
    public int? ParentId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
internal class GetAllCatgoryesQueryHandler : IRequestHandler<GetAllCategoryQuery, PaginatedResult<GetCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCatgoryesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PaginatedResult<GetCategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
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

        var map = _mapper.Map<List<GetCategoryDto>>(query);

        return PaginatedResult<GetCategoryDto>.Create(map, count, request.PageNumber, request.PageSize);
    }
}
