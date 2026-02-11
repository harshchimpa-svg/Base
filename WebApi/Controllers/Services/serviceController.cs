using Application.Features.Services.Command;
using Application.Features.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Services

{
    [Route("api/services")]
    [ApiController]

    public class serviceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public serviceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateServiceCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateServiceCommand command)
        {
            var result = await _mediator.Send(new UpdateServiceCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var Services = await _mediator.Send(new GetAllServiceQueries());
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var Services = await _mediator.Send(new GetServiceByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var Services = await _mediator.Send(new DeleteServiceCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}