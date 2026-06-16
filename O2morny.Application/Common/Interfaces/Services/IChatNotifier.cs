namespace O2morny.Application.Common.Interfaces.Services
{
    public interface IChatNotifier
    {
        Task SendMessageToUserAsync(string userId, object message);
    }
}
