using Application.Dto.Vendors;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Vendors;
using MediatR;
using Shared;

namespace Application.Features.Vendors.Queries;

public class GetAllVendorQuery: IRequest<Result<List<GetVendorDto>>>
{
}
internal class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorQuery, Result<List<GetVendorDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllVendorsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetVendorDto>>> Handle(GetAllVendorQuery request, CancellationToken cancellationToken) 
    {
        var Vendor = await _unitOfWork.Repository<Vendor>().GetAll();

        var map = _mapper.Map<List<GetVendorDto>>(Vendor);

        return Result<List<GetVendorDto>>.Success(map, "Vendor list");
    }
}