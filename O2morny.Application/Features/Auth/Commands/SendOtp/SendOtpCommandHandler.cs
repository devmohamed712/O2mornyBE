using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Extensions;
using O2morny.Application.Common.Helpers;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Domain.Common.Entities;
using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Features.Auth
{
    public class SendOtpHandler : IRequestHandler<SendOtpCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IWhatsappService _whatsappService;
        private readonly IAuthService _authService;

        public SendOtpHandler(IApplicationDbContext applicationDbContext, IWhatsappService whatsappService, IAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _whatsappService = whatsappService;
            _authService = authService;
        }

        public async Task Handle(SendOtpCommand request, CancellationToken ct)
        {
            var phone = PhoneHelper.Normalize(request.PhoneNumber);

            var userId = await _authService.GetUserIdByPhone(phone);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var account = await _applicationDbContext.Accounts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == userId, ct);

                if (account is not null && !account.Status.CanLogin())
                {
                    throw new UnauthorizedAccessException("Account cannot login");
                }
            }

            var lastOtp = await _applicationDbContext.WhatsappOtps
                .AsNoTracking()
                .Where(x => x.PhoneNumber == phone && x.Status == OtpStatus.Sent)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(ct);

            if (lastOtp is not null && lastOtp.CreatedAt > DateTime.UtcNow.AddSeconds(-60))
            {
                throw new Exception("Please wait before requesting another OTP");
            }

            var now = DateTime.UtcNow;

            var otpCode = Random.Shared.Next(100000, 999999).ToString();

            var otp = new WhatsappOtp
            {
                PhoneNumber = phone,
                Code = otpCode,
                CreatedAt = now,
                ExpireAt = now.AddMinutes(5),
                IsUsed = false,
                Status = OtpStatus.Pending
            };

            await _applicationDbContext.WhatsappOtps.AddAsync(otp, ct);

            await _applicationDbContext.SaveChangesAsync(ct);

            try
            {
                await _whatsappService.SendOtp(phone, otpCode);

                otp.Status = OtpStatus.Sent;
            }
            catch
            {
                otp.Status = OtpStatus.Failed;
                throw;
            }

            await _applicationDbContext.SaveChangesAsync(ct);
        }
    }
}
