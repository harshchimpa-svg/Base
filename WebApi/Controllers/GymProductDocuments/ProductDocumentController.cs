using Application.Features.GymDocuments.Command;
using Application.Features.GymDocuments.Queries;
using Application.Features.ProductDocuments.Command;
using Application.Features.ProductDocuments.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymProductDocuments
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductDocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDocument([FromForm] CreateProductDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductDocument(int id, [FromForm] CreateProductDocumentCommand command)
        {
            var result = await _mediator.Send(new UpdateProductDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] GetAllProductDocumentQuery query)
        {
            var gyms = await _mediator.Send(query);
            return Ok(gyms);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDocumentById(int id)
        {
            var result = await _mediator.Send(new GetProductDocumentByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDocument(int id)
        {
            var result = await _mediator.Send(new DeleteProductDocumentCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}
   
