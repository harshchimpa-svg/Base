using Application.Features.Trainers.Commands;
using Application.Features.Trainers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/trainer")]
public class TrainerController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrainerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTrainerCommand command)
    {
        var result = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(result);
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update( UpdateTrainerCommand command)
    {
        var result = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(result);
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTrainerQuery query)
    {
        var result = await _mediator.Send(query);
        return ResponseHelper.GenerateResponse(result);
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetTrainerByIdQuery(id));
        return ResponseHelper.GenerateResponse(result);
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteTrainerCommand(id));
        return ResponseHelper.GenerateResponse(result);
    }
}