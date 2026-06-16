using Microsoft.AspNetCore.Builder;

namespace O2morny.Infrastructure.Persistence.Seed
{
    public static class ApplicationSeeder
    {
        public static async Task SeedAsync(this WebApplication app, CancellationToken ct)
        {
            await app.SeedRolesAsync();
            await app.SeedCountriesAsync(ct);
            await app.SeedCitiesAsync(ct);
            await app.SeedAdminAsync(ct);
        }
    }
}
