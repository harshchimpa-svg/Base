using Application.Features.GymDocuments.Command;
using Application.Features.GymDocuments.Queries;
using Application.Features.ProductDocuments.Command;
using Application.Features.ProductDocuments.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymProductDocuments
{
    [Route("api/product-documents")]
    [ApiController]
    public class ProductDocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductDocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateProductDocumentCommand command)
        {
            var result = await _mediator.Send(new UpdateProductDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductDocumentQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductDocumentByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductDocumentCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}
   
