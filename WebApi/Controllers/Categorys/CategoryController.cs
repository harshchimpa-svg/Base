using Application.Features.Categoryes.Command;
using Application.Features.Categoryes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Categoryes
{
    [Route("api/categoryes")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoriCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] CreateCategoriCommand command)
        {
            var result = await _mediator.Send(new UpdateCategoriCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCategory([FromQuery] GetAllCategoriQuery query)
        {
            var Categoryes = await _mediator.Send(query);
            return Ok(Categoryes);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoriByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoriCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}