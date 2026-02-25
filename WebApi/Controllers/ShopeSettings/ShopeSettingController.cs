using Application.Features.Services.Command;
using Application.Features.Services.Queries;
using Application.Features.ShopeSettings.Command;
using Application.Features.ShopeSettings.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.ShopeSettings

{
    [Route("api/shopesetting")]
    [ApiController]

    public class ShopeSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopeSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles =  "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateShopeSettingCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        [Authorize(Roles =  "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateShopeSettingCommand command)
        {
            var result = await _mediator.Send(new UpdateShopeSettingCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var Services = await _mediator.Send(new GetAllShopeSettingQueries());
            return ResponseHelper.GenerateResponse(Services);
        }

        [Authorize(Roles =  "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var Services = await _mediator.Send(new GetShopeSettingByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var Services = await _mediator.Send(new DeleteShopeSettingCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}