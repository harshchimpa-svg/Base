using Application.Features.SaleStatus.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.SaleStatus
{
    [Route("api/sale-status")]
    [ApiController]

    public class SaleStatuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleStatuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateSaleStatusCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}