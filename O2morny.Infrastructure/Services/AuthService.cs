using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Exceptions;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Application.Common.Models;
using O2morny.Application.Features.Auth.DTOs;
using O2morny.Domain.Common.Enums;
using O2morny.Infrastructure.Persistence.Identity;

namespace O2morny.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwt;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt;
        }

        public async Task<string?> GetUserIdByPhone(string phone)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == phone);

            return user?.Id;
        }

        public async Task<string?> GetUserRoleById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return roles.FirstOrDefault();
        }

        public async Task<string> CreateUser(string phone)
        {
            var user = new ApplicationUser
            {
                UserName = phone,
                PhoneNumber = phone,
                PhoneNumberConfirmed = true
            };

            await _userManager.CreateAsync(user);

            return user.Id;
        }

        public async Task AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (!roleExists)
                throw new BadRequestException("Invalid role");

            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                throw new BadRequestException(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var roles = await _roleManager.Roles.Where(x => !x.Name.Equals(nameof(AccountRole.Admin)))
            .Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                EnName = r.EnName,
                ArName = r.ArName
            })
            .ToListAsync();

            return roles;
        }

        public async Task<string> GenerateJwt(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            var jwtUser = new JwtUserModel
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Roles = roles
            };

            return await _jwt.GenerateToken(jwtUser);
        }
    }
}
