using Application.Features.Sales.Command;
using Application.Features.Sales.Queries;
using Application.Features.Services.Command;
using Application.Features.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Sales

{
    [Route("api/sale")]
    [ApiController]

    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateSaleCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateSaleCommand command)
        {
            var result = await _mediator.Send(new UpdateSaleCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSaleQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var Services = await _mediator.Send(new GetSaleByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var Services = await _mediator.Send(new DeleateSaleCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}