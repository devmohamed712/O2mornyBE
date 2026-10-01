using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Application.Features.Account;
using O2morny.Domain.Common.Enums;

namespace O2morny.Application.Features.Auth
{
    public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, AuthResponse>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public VerifyOtpHandler(IApplicationDbContext applicationDbContext, IAuthService authService, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(VerifyOtpCommand request, CancellationToken ct)
        {
            string? phone = PhoneNormalizer.Normalize(request.PhoneNumber);
            if (string.IsNullOrEmpty(phone))
            {
                throw new Exception("Phone number isn't valid");
            }

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

            var userId = await _authService.GetUserIdByPhone(phone);

            if (userId == null)
            {
                userId = await _authService.CreateUser(phone);
                await _authService.AssignRoleAsync(userId, nameof(AccountRole.User));
            }

            await _applicationDbContext.SaveChangesAsync(ct);

            var token = await _authService.GenerateJwt(userId);

            var account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.Id == userId, ct);

            var role = await _authService.GetUserRoleById(userId);

            return new AuthResponse
            {
                Token = token,
                Role = role,
                Account = account != null ? _mapper.Map<AccountDto>(account) : null
            };
        }
    }
}
