using Application.Features.Balence.Commands;
using Application.Features.Balence.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Balance

{
    [Route("api/[controller]")]
    [ApiController]

    public class BalanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateVendor(CreateBalenceCommand command)
        {
            var Vendor = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, CreateBalenceCommand command)
        {
            var result = await _mediator.Send(new UpdateBalenceCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetVendor()
        {
            var Vendor = await _mediator.Send(new GetAllBalenceQuery());
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetVendorById(int id)
        {
            var Vendor = await _mediator.Send(new GetBalenceByIdQuery(id));
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var Vendor = await _mediator.Send(new DeleteBalenceCommand(id));
            return ResponseHelper.GenerateResponse(Vendor);
        }
    }
}