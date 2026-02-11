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

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]

        public async Task<ActionResult> CreateLocation(CreateLocationCommand command)
        {
            var location = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(location);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateLocation(int id, CreateLocationCommand command)
        {
            var result = await _mediator.Send(new UpdateLocationCommand(id, command));
            return ResponseHelper.GenerateResponse(result);

        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]

        public async Task<IActionResult> GetLocation()
        {
            var location = await _mediator.Send(new GetLocationQuery());
            return ResponseHelper.GenerateResponse(location);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]

        public async Task<ActionResult> GetLocationById(int id)
        {
            var location = await _mediator.Send(new GetByIdLocationQuery(id));
            return ResponseHelper.GenerateResponse(location);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _mediator.Send(new DeleteLocationCommand(id));
            return ResponseHelper.GenerateResponse(location);
        }
    }
}
