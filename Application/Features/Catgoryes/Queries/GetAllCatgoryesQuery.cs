using Application.Dto.Catgoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagoryes;
using MediatR;
using Shared;

namespace Application.Features.Catgoryes.Queries;

public class GetAllCatgoryesQuery : IRequest<Result<List<GetCatgoryDto>>>
{
}
internal class GetAllCatgoryesQueryHandler : IRequestHandler<GetAllCatgoryesQuery, Result<List<GetCatgoryDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCatgoryesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<GetCatgoryDto>>> Handle(GetAllCatgoryesQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Catgory>().GetAll();

        var map = _mapper.Map<List<GetCatgoryDto>>(locations);

        return Result<List<GetCatgoryDto>>.Success(map, "Location list");
    }
}