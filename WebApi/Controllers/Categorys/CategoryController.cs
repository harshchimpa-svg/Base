using Application.Features.Categoryes.Command;
using Application.Features.Categoryes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoriCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] CreateCategoriCommand command)
        {
            var result = await _mediator.Send(new UpdateCategoriCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetCategory([FromQuery] GetAllCategoriQuery query)
        {
            var Categoryes = await _mediator.Send(query);
            return Ok(Categoryes);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoriByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoriCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}