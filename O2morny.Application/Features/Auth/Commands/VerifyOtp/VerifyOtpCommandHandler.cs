using MediatR;
using O2morny.Application.Common.Helpers;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using O2morny.Application.Features.Account;

namespace O2morny.Application.Features.Auth
{
    public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, AuthResponse>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _auth;
        private readonly IMapper _mapper;

        public VerifyOtpHandler(IApplicationDbContext applicationDbContext, IAuthService auth, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _auth = auth;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(VerifyOtpCommand request, CancellationToken ct)
        {
            var phone = PhoneHelper.Normalize(request.PhoneNumber);

            var otp = await _applicationDbContext.WhatsappOtps.FirstOrDefaultAsync(x =>
                    x.PhoneNumber == phone &&
                    x.Code == request.OTP &&
                    !x.IsUsed &&
                    x.ExpireAt > DateTime.UtcNow,
                    ct);
            ;

            if (otp == null)
                throw new Exception("Invalid OTP");

            otp.IsUsed = true;

            var userId = await _auth.GetUserIdByPhone(phone);

            if (userId == null)
            {
                userId = await _auth.CreateUser(phone);
            }

            await _applicationDbContext.SaveChangesAsync(ct);

            var token = await _auth.GenerateJwt(userId);

            var account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.Id == userId, ct);

            var role = await _auth.GetUserRoleById(userId);

            return new AuthResponse
            {
                Token = token,
                Role = role,
                Account = account != null ? _mapper.Map<AccountDto>(account) : null
            };
        }
    }
}
