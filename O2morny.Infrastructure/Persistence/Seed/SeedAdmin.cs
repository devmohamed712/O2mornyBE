using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using O2morny.Domain.Common.Enums;
using O2morny.Infrastructure.Persistence.Identity;
using O2morny.Infrastructure.Settings;

namespace O2morny.Infrastructure.Persistence.Seed
{
    public static class SeedAdmin
    {
        public static async Task SeedAdminAsync(this WebApplication app, CancellationToken ct)
        {
            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var adminSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminSettings>>();
            var context = scope.ServiceProvider.GetRequiredService<O2mornyContext>();

            var adminRoleName = "Admin";

            // 1. Ensure Role exists
            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                var roleResult = await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = adminRoleName,
                    EnName = "Admin",
                    ArName = "مدير"
                });

                if (!roleResult.Succeeded)
                    throw new Exception("Failed to create Admin role");
            }

            // 2. Loop on phones
            foreach (var phone in adminSettings.Value.AdminPhones)
            {
                var normalizedPhone = phone.Trim();

                var user = await userManager.FindByNameAsync(normalizedPhone);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = normalizedPhone,
                        PhoneNumber = normalizedPhone,
                        PhoneNumberConfirmed = true
                    };

                    var createResult = await userManager.CreateAsync(user);

                    if (!createResult.Succeeded)
                        throw new Exception($"Failed to create admin user: {normalizedPhone}");

                    await context.Accounts.AddAsync(new Domain.Common.Entities.Account
                    {
                        Id = user.Id,
                        Name = "Admin",
                        Address = "Alexandria",
                        CityId = 3,
                        CreatedAt = DateTime.UtcNow,
                        DateOfBirth = new DateTime(1994, 7, 15),
                        HideBirthDate = true,
                        NationalId = user.UserName,
                        IsAcceptPrivacy = true,
                        IsAcceptTerms = true,
                        Status = AccountStatus.Active
                    }, ct);
                    await context.SaveChangesAsync(ct);
                }

                // 3. Assign role safely
                if (!await userManager.IsInRoleAsync(user, adminRoleName))
                {
                    var roleResult = await userManager.AddToRoleAsync(user, adminRoleName);

                    if (!roleResult.Succeeded)
                        throw new Exception($"Failed to assign role to: {normalizedPhone}");
                }
            }
        }
    }
}
