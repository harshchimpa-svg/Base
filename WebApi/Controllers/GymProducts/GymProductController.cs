using Application.Features.GymProducts.Command;
using Application.Features.GymProducts.Queries;
using Application.Features.Gyms.Command;
using Application.Features.Gyms.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymProducts
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GymProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]

        public async Task<ActionResult> CreateGymProduct(CreateGymProductCommand command)
        {
            var gym = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateGymProduct(int id, CreateGymProductCommand command)
        {
            var result = await _mediator.Send(new UpdateGymProductCommand(id, command));
            return ResponseHelper.GenerateResponse(result);

        }
        [HttpGet]

        public async Task<IActionResult> GetGymProduct()
        {
            var gym = await _mediator.Send(new GetGymProductQuery());
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetGymProductById(int id)
        {
            var gym = await _mediator.Send(new GetByIdGymProductQuery(id));
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteGymProduct(int id)
        {
            var gym = await _mediator.Send(new DeleteGymProductCommand(id));
            return ResponseHelper.GenerateResponse(gym);
        }
    }
}

