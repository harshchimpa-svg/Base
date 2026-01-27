using Application.Features.Categoryes.Queries;
using Application.Features.Dashboards.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DashBoardes
{
    [Route("api/dashBoardes")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocation()
        {
            var location = await _mediator.Send(new GetAllDashBoardQuery());
            return ResponseHelper.GenerateResponse(location);
        }
    }
}
