using Application.Features.Services.Command;
using Application.Features.Services.Queries;
using Application.Features.ShopSettings.Command;
using Application.Features.ShopSettings.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.ShopSettings

{
    [Route("api/shope-setting")]
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
        public async Task<ActionResult> Create(CreateShopSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateShopSettingCommand command)
        {
            var result = await _mediator.Send(new UpdateShopSettingCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllShopSettingQuery());
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetShopSettingByIdQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [Authorize(Roles =  "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteShopSettingCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}