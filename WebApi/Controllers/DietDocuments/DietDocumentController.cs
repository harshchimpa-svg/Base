using Application.Features.DietDocuments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DietDocuments
{

    [Route("api/diet-documents")]
    [ApiController]
    public class DietDocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DietDocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiet(CreateDiteDocumentCommand command)
        {
            var diet = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(diet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiet(int id, CreateDiteDocumentCommand command)
        {
            var result = await _mediator.Send(new UpdateDiteDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}