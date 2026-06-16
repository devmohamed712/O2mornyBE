using MediatR;

namespace O2morny.Application.Features.Chat
{
    public class SendMessageCommand : IRequest<bool>
    {
        public string ReceiverId { get; set; }

        public string Content { get; set; }
    }
}
