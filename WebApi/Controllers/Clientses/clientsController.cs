using Application.Features.Clientses.Command;
using Application.Features.Clientses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Clientses

{
    [Route("api/[controller]")]
    [ApiController]

    public class clientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public clientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateClients(CreateClientsCommand command)
        {
            var Clients = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClients(int id, CreateClientsCommand command)
        {
            var result = await _mediator.Send(new UpdateClientsCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var Clients = await _mediator.Send(new GetAllClientsQueries());
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientsById(int id)
        {
            var Clients = await _mediator.Send(new GetClientsByIdQueries(id));
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClients(int id)
        {
            var Clients = await _mediator.Send(new DeleteClientsCommand(id));
            return ResponseHelper.GenerateResponse(Clients);
        }
    }
}