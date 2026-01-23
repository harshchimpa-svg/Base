using Application.Features.DiteTypes.Command;
using Application.Features.DiteTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DiteTypes
{
    [Route("api/[controller]")]
    [ApiController]

    public class DiteTypesController: ControllerBase
    {
        private readonly IMediator _mediator;

        public DiteTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateDiteTypeCommands command)
        {
            var services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(services);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateDiteTypeCommands command)
        {
            var result = await _mediator.Send(new UpdateDiteTypesCommands(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var result = await _mediator.Send(new GetAllDiteTypeQuery());
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var result = await _mediator.Send(new GetDiteTypeByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var result = await _mediator.Send(new DeleateDiteTypeCommands(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}