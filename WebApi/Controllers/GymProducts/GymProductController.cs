using Application.Features.GymProducts.Command;
using Application.Features.GymProducts.Queries;
using Application.Features.Gyms.Command;
using Application.Features.Gyms.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymProducts
{
    [Route("api/gym-products")]
    [ApiController]
    public class GymProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GymProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]

        public async Task<ActionResult> Create(CreateGymProductCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, CreateGymProductCommand command)
        {
            var result = await _mediator.Send(new UpdateGymProductCommand(id, command));
            return ResponseHelper.GenerateResponse(result);

        }
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllGymProductQuery());
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetByIdGymProductQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteGymProductCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}

