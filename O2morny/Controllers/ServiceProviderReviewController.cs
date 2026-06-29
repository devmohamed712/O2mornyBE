using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using O2morny.Application.Features.ServiceProviderReview;
using O2morny.Domain.Common.Enums;
using System.Security.Claims;

namespace O2morny.API.Controllers
{
    [ApiController]
    [Route("api/serviceProviderReview")]
    [Authorize(Roles = $"{nameof(AccountRole.Admin)},{nameof(AccountRole.Client)}")]
    public class ServiceProviderReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceProviderReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetServiceProviderReviewsQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetServiceProviderReviewByIdQuery
            {
                Id = id
            }, ct);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceProviderReviewCommand command, CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            command.ClientAccountId = userId;

            var serviceProviderReview = await _mediator.Send(command, ct);

            return Ok(serviceProviderReview);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateServiceProviderReviewCommand command, CancellationToken ct)
        {
            var serviceProviderReview = await _mediator.Send(command, ct);

            return Ok(serviceProviderReview);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteServiceProviderReviewCommand
            {
                Id = id
            }, ct);

            return NoContent();
        }
    }
}
