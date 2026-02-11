using Application.Features.Abouts.Commands;
using Application.Features.Abouts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Abouts
{
    [Route("api/about")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AboutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout([FromForm] CreateAboutCommand command)
        {
            var about = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(about);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbout(int id, [FromForm] CreateAboutCommand command)
        {
            var result = await _mediator.Send(new UpdateAboutCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAbout()
        {
            var about = await _mediator.Send(new GetAllAboutQuery());
            return ResponseHelper.GenerateResponse(about);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(int id)
        {
            var about = await _mediator.Send(new GetAboutByIdQuery(id));
            return ResponseHelper.GenerateResponse(about);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var about = await _mediator.Send(new DeleateAboutCommand(id));
            return ResponseHelper.GenerateResponse(about);
        }
    }
}