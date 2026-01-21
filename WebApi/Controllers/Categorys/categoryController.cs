using Application.Features.Catgoryes.Command;
using Application.Features.Catgoryes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Catgoryes
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public categoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatgory([FromForm] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatgory(int id, [FromForm] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetChair([FromQuery] GetAllCategoryQuery query)
        {
            var Catgoryes = await _mediator.Send(query);
            return Ok(Catgoryes);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatgoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatgory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}