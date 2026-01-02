using Application.Features.Catgoryes.Command;
using Application.Features.Documents.Commands;
using Application.Features.Documents.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Documents;

[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateDocumentCommand command)
    {
        var result = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateDocumentDto image)
    {
        var result = await _mediator.Send(new UpdateDocumentCommand(id, image));

        return ResponseHelper.GenerateResponse(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetDocumentQuery());
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetByIdDocumentQuery(id));

        return ResponseHelper.GenerateResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(
            new DeleteDocumentCommand(id));

        return ResponseHelper.GenerateResponse(result);
    }
}
