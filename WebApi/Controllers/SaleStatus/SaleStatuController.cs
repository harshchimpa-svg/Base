using Application.Features.SaleStatus.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.SaleStatus
{
    [Route("api/SaleStatus")]
    [ApiController]

    public class SaleStatuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleStatuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateSaleStatusCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}