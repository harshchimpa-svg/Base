using Application.Features.Balence.Command;
using Application.Features.Balence.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Transaction
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateTransactionCommand command)
        {
            var transaction = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(transaction);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateTransactionCommand command)
        {
            var result = await _mediator.Send(new UpdateTransactionCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTransactionQuery query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTransactionById(int id)
        {
            var transaction = await _mediator.Send(new GetTransactionByIdQuery(id));
            return ResponseHelper.GenerateResponse(transaction);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("recent-activity")]
        public async Task<IActionResult> GetRecentActivity([FromQuery] GetRecentActivityQuery query)
        {
            var result = await _mediator.Send(query);
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _mediator.Send(new DeleteTransactionCommand(id));
            return ResponseHelper.GenerateResponse(transaction);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("all/{customerId}")]
        public async Task<IActionResult> DeleteAllByCustomer(int customerId)
        {
            var result = await _mediator.Send(new DeleteAllTransationCommand(customerId));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}