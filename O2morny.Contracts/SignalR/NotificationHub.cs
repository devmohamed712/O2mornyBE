using Microsoft.AspNetCore.SignalR;

namespace O2morny.Contracts.SignalR
{
    public class NotificationHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;
    }
}
