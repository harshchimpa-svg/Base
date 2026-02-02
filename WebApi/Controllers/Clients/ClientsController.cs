using Application.Features.Clientses.Command;
using Application.Features.Clientses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Clientses

{
    [Route("api/clients")]
    [ApiController]

    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateClients(CreateClientCommand command)
        {
            var Clients = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClients(int id, CreateClientCommand command)
        {
            var result = await _mediator.Send(new UpdateClientCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllClientQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientsById(int id)
        {
            var Clients = await _mediator.Send(new GetClientByIdQueries(id));
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClients(int id)
        {
            var Clients = await _mediator.Send(new DeleteClientCommand(id));
            return ResponseHelper.GenerateResponse(Clients);
        }
    }
}