using Application.Features.Categories.Command;
using Application.Features.Categories.Queries;
using Application.Features.Categoryes.Command;
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
        public async Task<IActionResult> Create([FromForm] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCategoryQuery query)
        {
            var Categoryes = await _mediator.Send(query);
            return Ok(Categoryes);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}