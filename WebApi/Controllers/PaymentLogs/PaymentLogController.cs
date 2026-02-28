using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using Application.Features.PaymentLoges.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.PaymentLoges
{
    [Route("api/paymentLoge")]
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
            return Ok(result);
        }
    }
}