using Application.Features.GymDocuments.Command;
using Application.Features.GymDocuments.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymDocuments
{
    [Route("api/gym-document")]
    [ApiController]
    public class GymDocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GymDocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGymDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateGymDocumentCommand command)
        {
            var result  = await _mediator.Send(new UpdateGymDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllGymDocumentQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetGymDocumentByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteGymDocumentCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}
