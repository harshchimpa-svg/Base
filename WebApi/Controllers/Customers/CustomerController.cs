using Application.Features.Customers.Command;
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
        
        [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create([FromForm]CreateCustomerCommand command)
        {
            var customer = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(customer);
        }

        [HttpPost("Reminder")]
        public async Task<ActionResult> CreateReminder(ReminderCustomer command)
        {
            var customer = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(customer);
        }
        
        
        
        [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm]  CreateCustomerCommand command)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}/block")]
        public async Task<IActionResult> Block(int id, BlockCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCustomerQuery query)
        {
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }
        
        [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}