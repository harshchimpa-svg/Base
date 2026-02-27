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

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateDiteDocumentCommand command)
        {
            var DietDocuments = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(DietDocuments);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateDiteDocumentCommand command)
        {
            var result = await _mediator.Send(new UpdateDiteDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}