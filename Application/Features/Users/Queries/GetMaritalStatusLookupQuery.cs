using Application.Dto.CommonDtos;
using AutoMapper;
using Domain.Common.Enums.Employees;
using MediatR;
using Shared;

namespace Application.Features.Employees.Queries;

public class GetMaritalStatusLookupQuery : IRequest<Result<List<IdAndNameDto>>>;

internal class GetMaritalStatusLookupQueryHandler : IRequestHandler<GetMaritalStatusLookupQuery, Result<List<IdAndNameDto>>>
{
    private readonly IMapper _mapper;

    public GetMaritalStatusLookupQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<Result<List<IdAndNameDto>>> Handle(GetMaritalStatusLookupQuery request, CancellationToken cancellationToken)
    {
        var statuses = Enum.GetValues(typeof(MaritalStatus))
            .Cast<MaritalStatus>()
            .ToList();

        var mapData = _mapper.Map<List<IdAndNameDto>>(statuses);

        return Result<List<IdAndNameDto>>.Success(mapData, "Marital Statuses");
    }
}
