
using O2morny.Application.Features.Auth.DTOs;

namespace O2morny.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string?> GetUserIdByPhone(string phone);

        Task<string?> GetUserRoleById(string userId);

        Task<string> CreateUser(string phone);

        Task AssignRoleAsync(string userId, string role);

        Task<List<RoleDto>> GetRoles();

        Task<string> GenerateJwt(string userId);
    }
}
