using Application.Features.Clientses.Command;
using Application.Features.Clientses.Queries;
using Application.Features.Employees.Commands;
using Application.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles =  "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateClients(CreateEmployeeCommand command)
        {
            var Clients = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Clients);
        }
        
        [Authorize(Roles =  "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( UpdateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [Authorize(Roles =  "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var Clients = await _mediator.Send(new GetAllEmployeeQueries());
            return ResponseHelper.GenerateResponse(Clients);
        }

        [Authorize(Roles =  "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientsById(int id)
        {
            var Clients = await _mediator.Send(new GetEmployeeByIdQueries(id));
            return ResponseHelper.GenerateResponse(Clients);
        }

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClients(int id)
        {
            var Clients = await _mediator.Send(new DeleateEmployeeCommand(id));
            return ResponseHelper.GenerateResponse(Clients);
        }*/
    }
}