using Application.Features.Services.Command;
using Application.Features.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Services

{
    [Route("api/[controller]")]
    [ApiController]

    public class serviceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public serviceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateServiceCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateServiceCommand command)
        {
            var result = await _mediator.Send(new UpdateServicesCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var Services = await _mediator.Send(new GetAllServicesQueries());
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var Services = await _mediator.Send(new GetServicesByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var Services = await _mediator.Send(new DeleteServicesCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}