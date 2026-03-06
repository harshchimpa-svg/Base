using Application.Features.GymCartItems.Queries;
using Application.Features.GymCartsItems.Command;
using Application.Features.GymCartsItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GymCartItems
{
    [Route("api/cart-item")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<ActionResult> Create(CreateCartItemCommand command)
        {
            var result = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateCartItemCommand command)
        {
            var result = await _mediator.Send(new UpdateCartItemCommand(id, command));
            return ResponseHelper.GenerateResponse(result);

        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllCartItemQuery());
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetByIdCartItemQuery(id));
            return ResponseHelper.GenerateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCartItemCommand(id));
            return ResponseHelper.GenerateResponse(result);
        }
    }
}
