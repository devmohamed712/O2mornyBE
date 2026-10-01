using MediatR;
using Microsoft.AspNetCore.Mvc;
using O2morny.Application.Features.Auth;

namespace O2morny.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpCommand cmd, CancellationToken ct)
        {
            await _mediator.Send(cmd, ct);
            return Ok();
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand cmd, CancellationToken ct)
        {
            var result = await _mediator.Send(cmd, ct);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles(CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetRolesQuery(),
                ct);

            return Ok(result);
        }
    }
}
