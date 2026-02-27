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
        public async Task<ActionResult> Create(CreateContactCommand command)
        {
            var Contacts = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(Contacts);
        }

        // [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Contacts = await _mediator.Send(new GetAllContactQuery());
            return ResponseHelper.GenerateResponse(Contacts);
        }
    }
}