using Application.Features.ExerciseDocuments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.ExerciseDocuments
{

    [Route("api/Exercise-documents")]
    [ApiController]
    public class ExerciseDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExerciseDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiet(CreateExerciseDocumentCommand command)
        {
            var diet = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(diet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiet(int id, CreateExerciseDocumentCommand command)
        {
            var result = await _mediator.Send(new UpdateExerciseDocumentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}