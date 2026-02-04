using Application.Features.GymCategorys.Command;
using Application.Features.GymCategorys.Queries;
using Application.Features.Gyms.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymCategorys
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GymCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGymCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( int id, [FromBody] UpdateGymCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteGymCategoryCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]

        public async Task<IActionResult> GetAllGymCategory()
        {
            var gym = await _mediator.Send(new GetAllGymCategoryQuery());
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetGymCategoryById(int id)
        {
            var gym = await _mediator.Send(new GetByIdGymCategoryQuery(id));
            return ResponseHelper.GenerateResponse(gym);
        }
    }
}

