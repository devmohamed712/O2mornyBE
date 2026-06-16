using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using O2morny.Application.Features.Country;
using O2morny.Domain.Common.Enums;

namespace O2morny.API.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCountriesQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCountryByIdQuery
            {
                Id = id
            }, ct);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCountryCommand command, CancellationToken ct)
        {
            var country = await _mediator.Send(command, ct);

            return Ok(country);
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCountryCommand command, CancellationToken ct)
        {
            var country = await _mediator.Send(command, ct);

            return Ok(country);
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteCountryCommand
            {
                Id = id
            }, ct);

            return NoContent();
        }
    }
}
