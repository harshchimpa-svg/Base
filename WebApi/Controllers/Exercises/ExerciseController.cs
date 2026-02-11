using Application.Features.Exercises.Command;
using Application.Features.Exercises.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Exercises
{
    [Route("api/Exercise")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> CreateDiet(CreateExerciseCommand command)
        {
            var diet = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(diet);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiet(int id, CreateExerciseCommand command)
        {
            var result = await _mediator.Send(new UpdateExerciseCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetExerciseQuery query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDietById(int id)
        {
            var result = await _mediator.Send(new GetByIdExerciseQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiet(int id)
        {
            var result = await _mediator.Send(new DeleateExerciseCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}