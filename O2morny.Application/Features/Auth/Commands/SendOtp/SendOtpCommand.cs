using MediatR;

namespace O2morny.Application.Features.Auth
{
    public class SendOtpCommand : IRequest
    {
        public string PhoneNumber { get; set; }
    }
}
