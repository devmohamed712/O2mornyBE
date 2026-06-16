using Microsoft.AspNetCore.SignalR;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Infrastructure.Hubs;

namespace O2morny.Infrastructure.Services
{
    public class ChatNotifier : IChatNotifier
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public ChatNotifier(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageToUserAsync(string userId, object message)
        {
            await _hubContext.Clients
                .User(userId)
                .SendAsync("ReceiveMessage", message);
        }
    }
}
