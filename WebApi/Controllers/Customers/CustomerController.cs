using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromForm]CreateCustomerCommand command)
        {
            var customer = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(customer);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id,[FromForm]  CreateCustomerCommand command)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}/block")]
        public async Task<IActionResult> BlockCustomer(int id, BlockCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetCategory([FromQuery] GetAllCustomerQueries query)
        {
            var Categoryes = await _mediator.Send(query);
            return Ok(Categoryes);
        }
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQueries(id));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}