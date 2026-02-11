using Application.Features.Gyms.Command;
using Application.Features.Gyms.Queries;
using Application.Features.Sales.Command;
using Application.Features.Sales.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Gyms
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
    private readonly IMediator _mediator;

    public GymController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpPost]
    public async Task<ActionResult> CreateServices(CreateGymCommand command)
    {
        var Services = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(Services);
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateServices(int id, CreateGymCommand command)
    {
        var result = await _mediator.Send(new UpdateGymCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }
      
    // [Authorize(Roles =  "Admin,Employee")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetGymQuery query)
    {
        var data = await _mediator.Send(query);
        return Ok(data);
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetServicesById(int id)
    {
        var Services = await _mediator.Send(new GetByIdGymQuery(id));
        return ResponseHelper.GenerateResponse(Services);
    }

    // [Authorize(Roles =  "Admin,Employee")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServices(int id)
    {
        var Services = await _mediator.Send(new DeleteGymCommand(id));
        return ResponseHelper.GenerateResponse(Services);
    }
}
}
