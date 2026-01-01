using Application.Features.TranstionDocuments.Command;
using Application.Features.TranstionDocuments.Queries;
using Application.Features.TranstionDocuments.Queriesl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.TranstionDocuments;

[Route("api/[controller]")]
[ApiController]
public class TranstionDocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public TranstionDocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateLocation(CreateTranstionDocumentCommand command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, CreateTranstionDocumentCommand command)
    {
        var result = await _mediator.Send(new UpdateTranstionDocumentCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        var location = await _mediator.Send(new GetAllTranstionDocumentQuery());
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetLocationById(int id)
    {
        var location = await _mediator.Send(new GetTranstionDocumentByIdQuery(id));
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var location = await _mediator.Send(new DeleteTranstionDocumentCommand(id));
        return ResponseHelper.GenerateResponse(location);
    }
}
