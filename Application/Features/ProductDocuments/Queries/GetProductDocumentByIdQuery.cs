using Application.Dto.ProductDocuments;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.ProductDocuments;
using MediatR;
using Shared;

namespace Application.Features.ProductDocuments.Queries;

public class GetProductDocumentByIdQuery : IRequest<Result<GetProductDocument>>
{
    public int Id { get; set; }

    public GetProductDocumentByIdQuery(int id)
    {
        Id = id;
    }
}
internal class GetProductDocumentByIdQueryHandler : IRequestHandler<GetProductDocumentByIdQuery, Result<GetProductDocument>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public GetProductDocumentByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetProductDocument>> Handle(GetProductDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Repository<ProductDocument>().GetByID(request.Id);

        if (category == null)
        {
            return Result<GetProductDocument>.BadRequest("Product not found");
        }

        var mapdata = _mapper.Map<GetProductDocument>(category);

        return Result<GetProductDocument>.Success("Product not found");
    }
}