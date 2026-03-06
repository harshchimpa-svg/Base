using Application.Features.Dashboards.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Dashboards
{
    [Route("api/dash-board")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles =  "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllDashboardQuery());
            return ResponseHelper.GenerateResponse(result);
        }
    }
}
