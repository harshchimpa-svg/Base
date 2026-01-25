using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Customers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var customer = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CreateCustomerCommand command)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomerQueries());
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQueries(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}