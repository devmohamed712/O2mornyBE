using MediatR;

namespace O2morny.Application.Features.Auth
{
    public class VerifyOtpCommand : IRequest<AuthResponse>
    {
        public string PhoneNumber { get; set; }

        public string OTP { get; set; }
    }
}
