using Application.Features.Diets.Commands;
using Application.Features.Diets.Queries;
using Application.Features.DietTypes.Command;
using Application.Features.DietTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Diets
{
    [Route("api/diets")]
    [ApiController]
    public class DietController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DietController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiet(CreateDietCommand command)
        {
            var diet = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(diet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiet(int id, CreateDietCommand command)
        {
            var result = await _mediator.Send(new UpdateDietCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllDietQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDietById(int id)
        {
            var result = await _mediator.Send(new GetDietByIdQueries(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiet(int id)
        {
            var result = await _mediator.Send(new DeleateDietCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}