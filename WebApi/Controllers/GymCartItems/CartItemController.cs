using Application.Features.GymCartsItems.Command;
using Application.Features.GymCartsItems.Queries;
using Application.Features.Gyms.Command;
using Application.Features.Gyms.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymCartItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<ActionResult> CreateCartItem(CreateCartItemCommand command)
        {
            var gym = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCartItem(int id, CreateCartItemCommand command)
        {
            var result = await _mediator.Send(new UpdateCartItemCommand(id, command));
            return ResponseHelper.GenerateResponse(result);

        }
        [HttpGet]

        public async Task<IActionResult> GetCartItem()
        {
            var gym = await _mediator.Send(new GetCartItemQuery());
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetCartItemById(int id)
        {
            var gym = await _mediator.Send(new GetByIdCartItemQuery(id));
            return ResponseHelper.GenerateResponse(gym);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var gym = await _mediator.Send(new DeleteCartItemCommand(id));
            return ResponseHelper.GenerateResponse(gym);
        }
    }
}
