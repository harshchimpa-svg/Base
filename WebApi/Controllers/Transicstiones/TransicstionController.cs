using Application.Features.Transicstiones.Commands;
using Application.Features.Transicstiones.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Transicstiones;

[Route("api/[controller]")]
[ApiController]
public class TransicstionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransicstionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateLocation(CreateTransicstionCommand command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, CreateTransicstionCommand command)
    {
        var result = await _mediator.Send(new UpdateTransicstionCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        var location = await _mediator.Send(new GetAllTransicstionQuery());
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetLocationById(int id)
    {
        var location = await _mediator.Send(new GetTransicstionByIdQuery(id));
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var location = await _mediator.Send(new DeleteTransicstionCommand(id));
        return ResponseHelper.GenerateResponse(location);
    }
}
