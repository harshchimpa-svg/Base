using Application.Features.Tranners.Commands;
using Application.Features.Tranners.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrannerController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrannerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTranerCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update( UpdateTranerCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTrnerQuery query)
    {
        var data = await _mediator.Send(query);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetTranerByIdQuery(id));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteTranerCommand(id));
        return Ok(result);
    }
    
}