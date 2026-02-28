using Application.Features.SaleProducts.Command;
using Application.Features.SaleProducts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.SaleProducts

{
    [Route("api/sale-products")]
    [ApiController]

    public class SaleProductController: ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateSaleProductCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateSaleProductCommand command)
        {
            var result = await _mediator.Send(new UpdateSaleProductCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllSaleProductQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSaleProductByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSaleProductCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}