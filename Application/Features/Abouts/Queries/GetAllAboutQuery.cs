using Application.Dto.Abouts;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Abouts;
using Domain.Entities.Vendors;
using MediatR;
using Shared;

namespace Application.Features.Abouts.Queries;

public class GetAllAboutQuery: IRequest<Result<List<GetAboutDto>>>
{
}
internal class GetAllAboutQueryHandler : IRequestHandler<GetAllAboutQuery, Result<List<GetAboutDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAboutQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetAboutDto>>> Handle(GetAllAboutQuery request, CancellationToken cancellationToken) 
    {
        var Vendor = await _unitOfWork.Repository<About>().GetAll();

        var map = _mapper.Map<List<GetAboutDto>>(Vendor);

        return Result<List<GetAboutDto>>.Success(map, "Vendor list");
    }
}