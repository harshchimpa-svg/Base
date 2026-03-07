using Application.Features.Abouts.Command;
using Application.Features.Abouts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles =  "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAboutCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }
        
        [Authorize(Roles =  "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateAboutCommand command)
        {
            var result = await _mediator.Send(new UpdateAboutCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllAboutQuery());
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAboutByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAboutCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}