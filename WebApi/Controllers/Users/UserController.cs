using Application.Features.Employees.Queries;
using Application.Features.Users;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Features.Users.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var data = await _mediator.Send(new GetUserByIdQuery(id));
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserQuery query)
    {
        var data = await _mediator.Send(query);
        return Ok(data);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var data = await _mediator.Send(new GetCurrentUserQuery());
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationCommand command)
    {
        var data = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromForm]UpdateUserCommand command)
    {
        command.Id = id;
        var data = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpPut("current")]
    public async Task<IActionResult> UpdateCurrentUser([FromForm]UpdateCurrentUserCommand command)
    {
        var data = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var data = await _mediator.Send(new DeleteUserCommand(id));
        return ResponseHelper.GenerateResponse(data);
    }


}