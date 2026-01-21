using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Services;
using MediatR;
using Shared;

namespace Application.Features.Services.Command;

public class CreateServiceCommand: IRequest<Result<string>>, ICreateMapFrom<Service>
{
    public int? CatgoryId { get; set; }
    public string Name { get; set; }
    public string SerialNo { get; set; }
    public int? VendorId { get; set; }
    public Decimal Mesurment { get; set; }
    public Decimal Price { get; set; }
}

internal class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var Service = _mapper.Map<Service>(request);

        await _unitOfWork.Repository<Service>().AddAsync(Service);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Service created successfully.");
    }
}

