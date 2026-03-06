using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using Application.Features.PaymentLogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.PaymentLogs
{
    [Route("api/paymentlog")]
    [ApiController]
    public class PaymentLogController: ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentLogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize(Roles =  "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPaymentLogQuery query)
        {
            var result = await _mediator.Send(query);
            return ResponseHelper.GenerateResponse(result);
        }
    }
}