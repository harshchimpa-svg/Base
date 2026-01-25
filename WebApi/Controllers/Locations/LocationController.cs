using Application.Features.Locations.Command;
using Application.Features.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Locations
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<ActionResult> CreateLocation(CreateLocationCommand command)
        {
            var location = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateLocation(int id, CreateLocationCommand command)
        {
            var result = await _mediator.Send(new UpdateLocationCommand(id, command));
            return ResponseHelper.GenerateResponse(result);

        }

        [HttpGet]

        public async Task<IActionResult> GetLocation()
        {
            var location = await _mediator.Send(new GetLocationQuery());
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetLocationById(int id)
        {
            var location = await _mediator.Send(new GetByIdLocationQuery(id));
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _mediator.Send(new DeleteLocationCommand(id));
            return ResponseHelper.GenerateResponse(location);
        }
    }
}
