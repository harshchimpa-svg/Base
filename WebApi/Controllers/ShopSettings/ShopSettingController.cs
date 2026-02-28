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

    public class ShopSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles =  "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateShopeSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateShopeSettingCommand command)
        {
            var result = await _mediator.Send(new UpdateShopeSettingCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllShopeSettingQuery());
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetShopeSettingByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteShopeSettingCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}