using Application.Features.Dites.Commands;
using Application.Features.Dites.Queries;
using Application.Features.DiteTypes.Command;
using Application.Features.DiteTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Dites
{
    [Route("api/[controller]")]
    [ApiController]

    public class DiteController: ControllerBase
    {
        private readonly IMediator _mediator;

        public DiteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateDiteCommand command)
        {
            var services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(services);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateDiteCommand command)
        {
            var result = await _mediator.Send(new UpdateDiteCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllDiteQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var result = await _mediator.Send(new GetDiteByIdQueries(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var result = await _mediator.Send(new DeleateDiteCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}