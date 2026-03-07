using Application.Dto.Vendors;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Categories;
using Domain.Entities.Vendors;
using MediatR;
using Shared;

namespace Application.Features.Vendors.Queries;

public class GetVendorByIdQuery: IRequest<Result<GetVendorDto>>
{
    public int Id { get; set; }

    public GetVendorByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, Result<GetVendorDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetVendorByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetVendorDto>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        var vendor = await _unitOfWork.Repository<Vendor>().GetByID(request.Id);

        if (vendor == null)
        {
            return Result<GetVendorDto>.BadRequest("Vendor not found.");
        }

        var mapData = _mapper.Map<GetVendorDto>(vendor);

        return Result<GetVendorDto>.Success(mapData, "Vendor");
    }
}