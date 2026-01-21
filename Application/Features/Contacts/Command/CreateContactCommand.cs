using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Contacts;
using MediatR;
using Shared;

namespace Application.Features.Contacts.Command;

public class CreateContactCommand: IRequest<Result<string>>, ICreateMapFrom<Contact>
{   
    public string Name { get; set; }
    public string Email { get; set; }
    public string Massage { get; set; }
}

internal class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<string>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var Contact = _mapper.Map<Contact>(request);

        await _unitOfWork.Repository<Contact>().AddAsync(Contact);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Contact created successfully.");
    }

}

