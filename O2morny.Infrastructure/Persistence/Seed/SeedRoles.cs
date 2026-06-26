using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using O2morny.Domain.Common.Enums;
using O2morny.Infrastructure.Persistence.Identity;

namespace O2morny.Infrastructure.Persistence.Seed
{
    public static class SeedRoles
    {
        public static async Task SeedRolesAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var roleManager =
                scope.ServiceProvider
                    .GetRequiredService<RoleManager<ApplicationRole>>();

            var roles = new List<ApplicationRole> {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = nameof(AccountRole.Admin),
                    NormalizedName = nameof(AccountRole.Admin).ToUpper(),
                    EnName = "Administrator",
                    ArName = "مدير النظام"
                },

                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = nameof(AccountRole.Client),
                    NormalizedName = nameof(AccountRole.Client).ToUpper(),
                    EnName = "Client",
                    ArName = "عميل"
                },

                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = nameof(AccountRole.ServiceProvider),
                    NormalizedName = nameof(AccountRole.ServiceProvider).ToUpper(),
                    EnName = "Service Provider",
                    ArName = "مقدم خدمة"
                }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name!))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}