using Application.Features.Catgoryes.Command;
using Application.Features.Catgoryes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Catgoryes
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatgoryesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatgoryesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLocation(CreateCatagoryesCommand command)
        {
            var location = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, CreateCatagoryesCommand command)
        {
            var result = await _mediator.Send(new UpdateCatagoryesCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLocation()
        {
            var location = await _mediator.Send(new GetAllCatgoryesQuery());
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLocationById(int id)
        {
            var location = await _mediator.Send(new GetCatgoryesByIdQuery(id));
            return ResponseHelper.GenerateResponse(location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _mediator.Send(new DeleteCatagoryesCommand(id));
            return ResponseHelper.GenerateResponse(location);
        }
    }
}
