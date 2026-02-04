using Application.Dto.Employees;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Employees;
using MediatR;
using Shared;

namespace Application.Features.Employees.Queries;

public class GetAllEmployeeQueries: IRequest<Result<List<GetEmployeeDTO>>>
{
}
internal class GetAllAboutQueryHandler : IRequestHandler<GetAllEmployeeQueries, Result<List<GetEmployeeDTO>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAboutQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetEmployeeDTO>>> Handle(GetAllEmployeeQueries request, CancellationToken cancellationToken) 
    {
        var Vendor = await _unitOfWork.Repository<Employee>().GetAll();

        var map = _mapper.Map<List<GetEmployeeDTO>>(Vendor);

        return Result<List<GetEmployeeDTO>>.Success(map, "Vendor list");
    }
}