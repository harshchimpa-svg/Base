using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Customers

{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateCustomerCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateCustomerCommand command)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var Services = await _mediator.Send(new GetAllCustomersQueries());
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var Services = await _mediator.Send(new GetCustomersByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var Services = await _mediator.Send(new DeleteCustomerCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}