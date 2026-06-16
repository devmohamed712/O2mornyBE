using MediatR;

namespace O2morny.Application.Features.Account
{
    public class DeleteAccountCommand : IRequest
    {
        public string Id { get; set; }
    }
}
