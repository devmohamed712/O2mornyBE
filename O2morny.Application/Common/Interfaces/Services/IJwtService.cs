using O2morny.Application.Common.Models;

namespace O2morny.Application.Common.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(JwtUserModel user);
    }
}
