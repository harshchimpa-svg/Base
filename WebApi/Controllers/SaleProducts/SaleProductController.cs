using Application.Features.SaleProducts.Command;
using Application.Features.SaleProducts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.SaleProducts

{
    [Route("api/SaleProducts")]
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
        public async Task<ActionResult> CreateServices(CreateSaleProductCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateSaleProductCommand command)
        {
            var result = await _mediator.Send(new UpdateSaleProductCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSaleProductQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var Services = await _mediator.Send(new GetSaleProductByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var Services = await _mediator.Send(new DeleateSaleProductCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}