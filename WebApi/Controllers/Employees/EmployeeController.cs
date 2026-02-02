using Application.Features.Clientses.Command;
using Application.Features.Clientses.Queries;
using Application.Features.Employees.Commands;
using Application.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Employees

{
    [Route("api/Employees")]
    [ApiController]

    public class EmployeeController: ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateClients(CreateEmployeeCommand command)
        {
            var Clients = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClients(int id, CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var Clients = await _mediator.Send(new GetAllEmployeeQueries());
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientsById(int id)
        {
            var Clients = await _mediator.Send(new GetEmployeeByIdQueries(id));
            return ResponseHelper.GenerateResponse(Clients);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClients(int id)
        {
            var Clients = await _mediator.Send(new DeleateEmployeeCommand(id));
            return ResponseHelper.GenerateResponse(Clients);
        }
    }
}