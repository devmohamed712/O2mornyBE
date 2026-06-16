using O2morny.Application.Features.Account;

namespace O2morny.Application.Features.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string? Role { get; set; }
        public AccountDto? Account { get; set; }
    }
}
