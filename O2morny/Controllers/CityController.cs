using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using O2morny.Application.Features.City;
using O2morny.Domain.Common.Enums;

namespace O2morny.API.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("country/{countryId:int}")]
        public async Task<IActionResult> GetAll(int countryId, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCitiesQuery() { CountryId = countryId }, ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCityByIdQuery
            {
                Id = id
            }, ct);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityCommand command, CancellationToken ct)
        {
            var country = await _mediator.Send(command, ct);

            return Ok(country);
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCityCommand command, CancellationToken ct)
        {
            var country = await _mediator.Send(command, ct);

            return Ok(country);
        }

        [Authorize(Roles = nameof(AccountRole.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteCityCommand
            {
                Id = id
            }, ct);

            return NoContent();
        }
    }
}
