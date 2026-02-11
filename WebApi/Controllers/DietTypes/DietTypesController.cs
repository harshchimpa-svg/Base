using Application.Features.DietTypes.Command;
using Application.Features.DietTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DietTypes
{
    [Route("api/diteTypes")]
    [ApiController]
    public class DietTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DietTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> CreateDietType(CreateDietTypeCommands command)
        {
            var dietType = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(dietType);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDietType(int id, CreateDietTypeCommands command)
        {
            var result = await _mediator.Send(new UpdateDietTypeCommands(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetDietTypes()
        {
            var result = await _mediator.Send(new GetAllDietTypeQuery());
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDietTypeById(int id)
        {
            var result = await _mediator.Send(new GetDietTypeByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDietType(int id)
        {
            var result = await _mediator.Send(new DeleateDietTypeCommands(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}