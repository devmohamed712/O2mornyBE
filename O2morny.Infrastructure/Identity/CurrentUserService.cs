using Microsoft.AspNetCore.Http;
using O2morny.Application.Common.Interfaces.Identity;
using System.Security.Claims;

namespace O2morny.Infrastructure.Identity
{

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public Guid? UserId
        {
            get
            {
                var value = User?
                    .FindFirst(ClaimTypes.NameIdentifier)?
                    .Value;

                return Guid.TryParse(value, out var id)
                    ? id
                    : null;
            }
        }

        public string? Role =>
            User?.FindFirst(ClaimTypes.Role)?.Value;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string role) =>
            Role == role;
    }
}
