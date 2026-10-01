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

            // 1. Ensure Role exists
            if (!await roleManager.RoleExistsAsync(nameof(AccountRole.Admin)))
            {
                var roleResult = await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = nameof(AccountRole.Admin),
                    NormalizedName = nameof(AccountRole.Admin).ToUpper(),
                    EnName = "Administrator",
                    ArName = "مدير النظام"
                });

                if (!roleResult.Succeeded)
                    throw new Exception("Failed to create Admin role");
            }

            // 2. Loop on phones
            foreach (var phonenum in adminSettings.Value.AdminPhones)
            {
                string? phone = PhoneNormalizer.Normalize(phonenum);
                if (string.IsNullOrEmpty(phone))
                {
                    throw new Exception("Phone number isn't valid");
                }

                var user = await userManager.FindByNameAsync(phone);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = phone,
                        PhoneNumber = phone,
                        PhoneNumberConfirmed = true
                    };

                    var createResult = await userManager.CreateAsync(user);

                    if (!createResult.Succeeded)
                        throw new Exception($"Failed to create admin user: {phone}");

                    await context.Accounts.AddAsync(new Domain.Common.Entities.Account
                    {
                        Id = user.Id,
                        Name = "Admin",
                        Address = "Alexandria",
                        CityId = 3,
                        CreatedAt = DateTime.UtcNow,
                        DateOfBirth = new DateTime(1994, 7, 15),
                        IsAcceptPrivacy = true,
                        IsAcceptTerms = true,
                        Status = AccountStatus.Active
                    }, ct);
                    await context.SaveChangesAsync(ct);
                }

                // 3. Assign role safely
                if (!await userManager.IsInRoleAsync(user, nameof(AccountRole.Admin)))
                {
                    var roleResult = await userManager.AddToRoleAsync(user, nameof(AccountRole.Admin));

                    if (!roleResult.Succeeded)
                        throw new Exception($"Failed to assign role to: {phone}");
                }
            }
        }
    }
}
