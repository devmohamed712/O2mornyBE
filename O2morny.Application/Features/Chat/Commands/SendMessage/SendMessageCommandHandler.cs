using MediatR;
using O2morny.Application.Common.Interfaces.Identity;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Domain.Common.Entities;

namespace O2morny.Application.Features.Chat
{
    public class SendMessageCommandHandler
        : IRequestHandler<
            SendMessageCommand,
            bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly ICurrentUserService _currentUser;

        private readonly IChatNotifier _chatNotifier;

        public SendMessageCommandHandler(IApplicationDbContext applicationDbContext,ICurrentUserService currentUser,IChatNotifier chatNotifier)
        {
            _applicationDbContext = applicationDbContext;
            _currentUser = currentUser;
            _chatNotifier = chatNotifier;
        }

        public async Task<bool> Handle(
            SendMessageCommand request,
            CancellationToken ct)
        {
            var message = new Message
            {
                SenderId = Convert.ToString(_currentUser.UserId),

                ReceiverId = request.ReceiverId,

                Content = request.Content,

                CreatedAt = DateTime.UtcNow
            };

            _applicationDbContext.Messages.Add(message);

            await _applicationDbContext.SaveChangesAsync(ct);

            var response = new
            {
                message.Id,
                message.SenderId,
                message.ReceiverId,
                message.Content,
                message.CreatedAt
            };

            // Receiver
            await _chatNotifier
                .SendMessageToUserAsync(
                    request.ReceiverId,
                    response);

            // Sender
            await _chatNotifier
                .SendMessageToUserAsync(
                    Convert.ToString(_currentUser.UserId),
                    response);

            return true;
        }
    }
}
