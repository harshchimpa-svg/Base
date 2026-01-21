using Application.Dto.Abouts;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Abouts;
using MediatR;
using Shared;

namespace Application.Features.Abouts.Queries;

public class GetAboutByIdQuery: IRequest<Result<GetAboutDto>>
{
    public int Id { get; set; }

    public GetAboutByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetAboutByIdQueryHandler : IRequestHandler<GetAboutByIdQuery, Result<GetAboutDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAboutByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetAboutDto>> Handle(GetAboutByIdQuery request, CancellationToken cancellationToken)
    {
        var Vendor = await _unitOfWork.Repository<About>().GetByID(request.Id);

        if (Vendor == null)
        {
            return Result<GetAboutDto>.BadRequest("About not found.");
        }

        var mapData = _mapper.Map<GetAboutDto>(Vendor);

        return Result<GetAboutDto>.Success(mapData, "About");
    }
}