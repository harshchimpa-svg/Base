using Application.Features.PaymentHeades.Command;
using Application.Features.PaymentHeades.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.PaymentHeades;

[Route("api/[controller]")]
[ApiController]
public class PaymentHeadController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentHeadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateLocation(CreatePaymentHeadCommand command)
    {
        var location = await _mediator.Send(command);
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, CreatePaymentHeadCommand command)
    {
        var result = await _mediator.Send(new UpdatePaymentHeadCommand(id, command));
        return ResponseHelper.GenerateResponse(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation()
    {
        var location = await _mediator.Send(new GetAllPaymentHeadQuery());
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetLocationById(int id)
    {
        var location = await _mediator.Send(new GetPaymentHeadByIdQuery(id));
        return ResponseHelper.GenerateResponse(location);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var location = await _mediator.Send(new DeletePaymentHeadCommand(id));
        return ResponseHelper.GenerateResponse(location);
    }
}
