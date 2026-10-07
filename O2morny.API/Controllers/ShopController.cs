using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using O2morny.Application.Features.Shop;
using O2morny.Domain.Common.Enums;

namespace O2morny.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetShopsQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetShopByIdQuery
            {
                Id = id
            }, ct);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateShopCommand command, CancellationToken ct)
        {
            var country = await _mediator.Send(command, ct);

            return Ok(country);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateShopCommand command, CancellationToken ct)
        {
            var country = await _mediator.Send(command, ct);

            return Ok(country);
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteShopCommand
            {
                Id = id
            }, ct);

            return NoContent();
        }
    }
}
