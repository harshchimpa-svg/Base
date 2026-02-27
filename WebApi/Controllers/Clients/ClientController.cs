using Application.Features.Clients.Command;
using Application.Features.Clientses.Command;
using Application.Features.Clientses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Clientses

{
    [Route("api/clients")]
    [ApiController]

    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateClientCommand command)
        {
            var Clients = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Clients);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateClientCommand command)
        {
            var result = await _mediator.Send(new UpdateClientCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllClientQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var Clients = await _mediator.Send(new GetClientByIdQueries(id));
            return ResponseHelper.GenerateResponse(Clients);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Clients = await _mediator.Send(new DeleteClientCommand(id));
            return ResponseHelper.GenerateResponse(Clients);
        }
    }
}