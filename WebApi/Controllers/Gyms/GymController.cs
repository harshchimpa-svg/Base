using Application.Features.Gyms.Command;
using Application.Features.Gyms.Queries;
using Application.Features.Locations.Command;
using Application.Features.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Gyms
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GymController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<ActionResult> CreateGym(CreateGymCommand command)
        {
            var gym = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateGym(int id, CreateGymCommand command)
        {
            var result = await _mediator.Send(new UpdateGymCommand(id, command));
            return ResponseHelper.GenerateResponse(result);

        }
        [HttpGet]

        public async Task<IActionResult> GetGym()
        {
            var gym = await _mediator.Send(new GetGymQuery());
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetGymById(int id)
        {
            var gym = await _mediator.Send(new GetByIdGymQuery(id));
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteGym(int id)
        {
            var gym = await _mediator.Send(new DeleteGymCommand(id));
            return ResponseHelper.GenerateResponse(gym);
        }
    }
}

