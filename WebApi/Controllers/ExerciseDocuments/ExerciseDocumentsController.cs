using Application.Features.ExerciseDocuments.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.ExerciseDocuments
{

    [Route("api/exercise-documents")]
    [ApiController]
    public class ExerciseDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateExerciseDocumentCommand command)
        {
            var diet = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(diet);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateExerciseDocumentCommand command)
        {
            var result = await _mediator.Send(new UpdateExerciseDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}