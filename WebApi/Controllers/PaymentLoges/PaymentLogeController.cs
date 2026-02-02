using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using Application.Features.PaymentLoges.Queries;
using MediatR;
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
        
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _mediator.Send(new GetAllPaymentLogeQueries());
            return ResponseHelper.GenerateResponse(result);
        }
    }
}