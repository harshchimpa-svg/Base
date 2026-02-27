using Application.Features.Vendors.Command;
using Application.Features.Vendors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Vendors

{
    [Route("api/vendors")]
    [ApiController]

    public class VendorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateVendorCommand command)
        {
            var Vendor = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateVendorCommand command)
        {
            var result = await _mediator.Send(new UpdateVendorCommand(id, command)); 
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Vendor = await _mediator.Send(new GetAllVendorQuery());
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var Vendor = await _mediator.Send(new GetVendorByIdQuery(id));
            return ResponseHelper.GenerateResponse(Vendor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Vendor = await _mediator.Send(new DeleteVendorCommand(id));
            return ResponseHelper.GenerateResponse(Vendor);
        }
    }
}