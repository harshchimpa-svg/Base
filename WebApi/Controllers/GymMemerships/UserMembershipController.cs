using Application.Features.GymMemerships;
using Application.Features.GymMemerships.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymMemerships
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMembershipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserMembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserMembershipCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CreateUserMembershipCommand command)
        {
            var result = await _mediator.Send(
                new UpdateUserMembershipCommand(id, command));

            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(
                new GetUserMembershipQuery());

            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(
                new GetUserMembershipByIdQuery(id));

            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeleteUserMembershipCommand(id));

            return ResponseHelper.GenerateResponse(result);
        }
    }
}
