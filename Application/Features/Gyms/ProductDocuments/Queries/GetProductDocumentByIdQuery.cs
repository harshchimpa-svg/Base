using Application.Dto.ProductDocuments;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ProductDocuments;
using MediatR;
using Shared;

namespace Application.Features.ProductDocuments.Queries;

public class GetProductDocumentDtoByIdQuery : IRequest<Result<GetProductDocumentDto>>
{
    public int Id { get; set; }

    public GetProductDocumentDtoByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetProductDocumentDtoByIdQueryHandler : IRequestHandler<GetProductDocumentDtoByIdQuery, Result<GetProductDocumentDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public GetProductDocumentDtoByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProductDocumentDto>> Handle(GetProductDocumentDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var productDocument = await _unitOfWork.Repository<ProductDocument>().GetByID(request.Id);

        if (productDocument == null)
        {
            return Result<GetProductDocumentDto>.BadRequest("Product not found");
        }

        var mapData = _mapper.Map<GetProductDocumentDto>(productDocument);

        return Result<GetProductDocumentDto>.Success("ProductDocument not found");
    }
}