using Application.Features.SalePayments.Command;
using Application.Features.SalePayments.Queries;
using Application.Features.SaleProducts.Command;
using Application.Features.SaleProducts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.SalePayments

{
    [Route("api/SalePayment")]
    [ApiController]

    public class SalePaymentController: ControllerBase
    {
        private readonly IMediator _mediator;

        public SalePaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> CreateServices(CreateSalePaymentsCommand command)
        {
            var Services = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServices(int id, CreateSalePaymentsCommand command)
        {
            var result = await _mediator.Send(new UpdateSalePaymentCommand(id, command));
            return ResponseHelper.GenerateResponse(result);
        }
        
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSalePaymentsQueries query)
        {
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServicesById(int id)
        {
            var Services = await _mediator.Send(new GetSalePaymentByIdQueries(id));
            return ResponseHelper.GenerateResponse(Services);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServices(int id)
        {
            var Services = await _mediator.Send(new DeleateSalePaymentCommand(id));
            return ResponseHelper.GenerateResponse(Services);
        }
    }
}