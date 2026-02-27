using Application.Features.Roles.Command;
using Application.Features.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.UserRoles;

[Route("api/roles")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleCommand command)
    {
        var data = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _mediator.Send(new GetAllRoleQuery());
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var data = await _mediator.Send(new GetRoleByIdQuery(id));
        return ResponseHelper.GenerateResponse(data);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreateRoleCommand command)
    {
        var data = await _mediator.Send(new UpdateRoleCommand(id, command));
        return ResponseHelper.GenerateResponse(data);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var data = await _mediator.Send(new DeleteRoleCommand(id));
        return ResponseHelper.GenerateResponse(data);

    }

    [HttpPut("/api/user/{id}/role")]
    public async Task<ActionResult> CreateUser(string id, [FromBody] List<string> roles)
    {
        var data = await _mediator.Send(new CreateUpdateUserRoleCommand(id, roles));
        return ResponseHelper.GenerateResponse(data);
    }

}
