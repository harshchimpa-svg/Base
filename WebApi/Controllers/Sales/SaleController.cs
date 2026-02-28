using Application.Features.Sales.Command;
using Application.Features.Sales.Queries;
using Application.Features.Services.Command;
using Application.Features.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Sales

{
    [Route("api/sales")]
    [ApiController]

    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateSaleCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateSaleCommand command)
        {
            var result = await _mediator.Send(new UpdateSaleCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllSaleQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSaleByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSaleCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}