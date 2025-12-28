using Application.Dto.Catgoryes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Catagoryes;
using MediatR;
using Shared;

namespace Application.Features.Catgoryes.Queries;

public class GetAllCatgoryesQuery : IRequest<Result<GetCatgoryDto>>
{
}
internal class GetAllCatgoryesQueryHandler : IRequestHandler<GetAllCatgoryesQuery, Result<GetCatgoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCatgoryesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetCatgoryDto>> Handle(GetAllCatgoryesQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Catgory>().GetAll();

        var map = _mapper.Map<GetCatgoryDto>(locations);

        return Result<GetCatgoryDto>.Success(map, "Location list");
    }
}