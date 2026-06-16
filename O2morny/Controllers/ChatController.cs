using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using O2morny.Application.Features.Chat;

namespace O2morny.API.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send(SendMessageCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
