using Application.Features.Balence.Commands;
using Application.Features.Balence.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Transaction
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateTransaction(CreateTransactionCommand command)
        {
            var transaction = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(transaction);
        }

        // [Authorize(Roles =  "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, CreateTransactionCommand command)
        {
            var result = await _mediator.Send(new UpdateTransactionCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTransactionQuery query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTransactionById(int id)
        {
            var transaction = await _mediator.Send(new GetTransactionByIdQuery(id));
            return ResponseHelper.GenerateResponse(transaction);
        }
        
        // [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _mediator.Send(new DeleteTransactionCommand(id));
            return ResponseHelper.GenerateResponse(transaction);
        }
    }
}