using Application.Dto.Catgoryes;
using Application.Dto.Vendors;
using Application.Features.Catgoryes.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Catagoryes;
using MediatR;
using Shared;

namespace Application.Features.Vendors.Queries;

public class GetVendorsByIdQuery: IRequest<Result<GetVendorDto>>
{
    public int Id { get; set; }

    public GetVendorsByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetVendorsByIdQueryHandler : IRequestHandler<GetVendorsByIdQuery, Result<GetVendorDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetVendorsByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetVendorDto>> Handle(GetVendorsByIdQuery request, CancellationToken cancellationToken)
    {
        var Vendor = await _unitOfWork.Repository<Category>().GetByID(request.Id);

        if (Vendor == null)
        {
            return Result<GetVendorDto>.BadRequest("Vendor not found.");
        }

        var mapData = _mapper.Map<GetVendorDto>(Vendor);

        return Result<GetVendorDto>.Success(mapData, "Vendor");
    }
}