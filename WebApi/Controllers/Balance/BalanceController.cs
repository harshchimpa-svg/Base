using Application.Features.Balence.Commands;
using Application.Features.Balence.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Balance
{
    [Route("api/balance")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBalance(CreateBalenceCommand command)
        {
            var balance = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(balance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBalance(int id, CreateBalenceCommand command)
        {
            var result = await _mediator.Send(new UpdateBalenceCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance()
        {
            var balance = await _mediator.Send(new GetAllBalenceQuery());
            return ResponseHelper.GenerateResponse(balance);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBalanceById(int id)
        {
            var balance = await _mediator.Send(new GetBalenceByIdQuery(id));
            return ResponseHelper.GenerateResponse(balance);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBalance(int id)
        {
            var balance = await _mediator.Send(new DeleteBalenceCommand(id));
            return ResponseHelper.GenerateResponse(balance);
        }
    }
}