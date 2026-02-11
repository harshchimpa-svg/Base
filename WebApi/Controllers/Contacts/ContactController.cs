using Application.Features.Contacts.Command;
using Application.Features.Contacts.Queries;
using Application.Features.Vendors.Command;
using Application.Features.Vendors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Contacts

{
    [Route("api/contacts")]
    [ApiController]

    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // [Authorize(Roles =  "Admin,Employee")]
        [HttpPost]
        public async Task<ActionResult> CreateContact(CreateContactCommand command)
        {
            var Vendor = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Vendor);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            var Vendor = await _mediator.Send(new GetAllContactQueries());
            return ResponseHelper.GenerateResponse(Vendor);
        }
    }
}