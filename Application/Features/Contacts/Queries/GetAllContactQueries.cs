using Application.Dto.Contacts;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Contacts;
using MediatR;
using Shared;

namespace Application.Features.Contacts.Queries;

public class GetAllContactQueries: IRequest<Result<List<GetContactDto>>>
{
}
internal class GetAllContactQueriesHandler : IRequestHandler<GetAllContactQueries, Result<List<GetContactDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllContactQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetContactDto>>> Handle(GetAllContactQueries request, CancellationToken cancellationToken)
    {
        var Clients = await _unitOfWork.Repository<Contact>().GetAll();

        var map = _mapper.Map<List<GetContactDto>>(Clients);

        return Result<List<GetContactDto>>.Success(map, "Clients list");
    }
}