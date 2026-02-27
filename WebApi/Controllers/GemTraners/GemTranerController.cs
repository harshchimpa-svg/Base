using Application.Features.GymTraners.Commands;
using Application.Features.GymTraners.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GemTraner

{
    [Route("api/gemtraners")]
    [ApiController]
    public class GemTranersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GemTranersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateGymTranerCommands command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateGymTranerCommands command)
        {
            var result = await _mediator.Send(new UpdateGymTranerCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Services = await _mediator.Send(new GetAllGymTranerQueries());
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var Services = await _mediator.Send(new GetGymTranerByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Services = await _mediator.Send(new DeleteGymTranerCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}