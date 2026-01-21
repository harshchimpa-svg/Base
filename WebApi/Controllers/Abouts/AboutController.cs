using Application.Features.Abouts.Commands;
using Application.Features.Abouts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Abouts
{
    [Route("api/[controller]")]
    [ApiController]

    public class AboutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AboutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateVendor(CreateAboutCommand command)
        {
            var Vendor = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, CreateAboutCommand command)
        {
            var result = await _mediator.Send(new UpdateAboutCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetVendor()
        {
            var Vendor = await _mediator.Send(new GetAllAboutQuery());
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetVendorById(int id)
        {
            var Vendor = await _mediator.Send(new GetAboutByIdQuery(id));
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var Vendor = await _mediator.Send(new DeleateAboutCommand(id));
            return ResponseHelper.GenerateResponse(Vendor);
        }
    }
}