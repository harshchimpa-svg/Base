using Application.Dto.Contacts;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Contacts;
using MediatR;
using Shared;

namespace Application.Features.Contacts.Queries;

public class GetAllContactQuery: IRequest<Result<List<GetContactDto>>>
{
}
internal class GetAllContactQueryHandler : IRequestHandler<GetAllContactQuery, Result<List<GetContactDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllContactQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetContactDto>>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
    {
        var contact = await _unitOfWork.Repository<Contact>().GetAll();

        var map = _mapper.Map<List<GetContactDto>>(contact);

        return Result<List<GetContactDto>>.Success(map, "Contact list");
    }
}