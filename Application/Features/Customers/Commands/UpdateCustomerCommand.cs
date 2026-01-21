using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Customers.Commands;

public class UpdateCustomerCommand: IRequest<Result<Customer>>
{

    public int Id { get; set; }
    public CreateCustomerCommand CreateCommand { get; set; } = new();

    public UpdateCustomerCommand(int id, CreateCustomerCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<Customer>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Customer>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {

        var Service = await _unitOfWork.Repository<Customer>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Service == null)
        {
            return Result<Customer>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, Service);

        await _unitOfWork.Repository<Customer>().UpdateAsync(Service);
        await _unitOfWork.Save(cancellationToken);

        return Result<Customer>.Success("Update Service...");
    }
}