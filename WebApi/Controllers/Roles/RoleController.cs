using Application.Features.Roles.Command;
using Application.Features.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.UserRoles;

[Route("api/role")]
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
        var result = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllRoleQuery());
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id));
        return ResponseHelper.GenerateResponse(result);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CreateRoleCommand command)
    {
        var result = await _mediator.Send(new UpdateRoleCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteRoleCommand(id));
        return ResponseHelper.GenerateResponse(result);

    }

    [HttpPut("/api/user/{id}/role")]
    public async Task<ActionResult> CreateUser(string id, [FromBody] List<string> roles)
    {
        var result = await _mediator.Send(new CreateUpdateUserRoleCommand(id, roles));
        return ResponseHelper.GenerateResponse(result);
    }

}
