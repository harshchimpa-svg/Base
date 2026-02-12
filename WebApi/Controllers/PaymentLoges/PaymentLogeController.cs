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
    public class PaymentLogeController: ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentLogeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPaymentLogeQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }
    }
}