using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using MediatR;
using Shared;

namespace Application.Features.Customers.Commands;

public class CreateCustomerCommand: IRequest<Result<string>>, ICreateMapFrom<Customer>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public string Notes { get; set; }
}

internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {   
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var Customer = _mapper.Map<Customer>(request);

        await _unitOfWork.Repository<Customer>().AddAsync(Customer);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Customer created successfully.");
    }
}