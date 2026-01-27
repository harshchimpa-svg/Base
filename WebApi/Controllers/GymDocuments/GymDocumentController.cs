using Application.Features.GymDocuments.Command;
using Application.Features.GymDocuments.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymDocuments
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymDocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GymDocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGymDocument([FromForm] CreateGymDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGymDocument(int id, [FromForm] CreateGymDocumentCommand command)
        {
            var result  = await _mediator.Send(new UpdateGymDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGym([FromQuery] GetAllGymDocumentQuery query)
        {
            var gyms = await _mediator.Send(query);
            return Ok(gyms);
        }
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetGymDocumentById(int id)
        {
            var result = await _mediator.Send(new GetGymDocumentByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymDocument(int id)
        {
            var result = await _mediator.Send(new DeleteGymDocumentCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}
