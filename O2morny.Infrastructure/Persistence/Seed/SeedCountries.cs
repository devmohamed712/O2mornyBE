using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Seed
{
    public static class SeedCountries
    {
        public static async Task SeedCountriesAsync(this WebApplication app, CancellationToken ct)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<O2mornyContext>();

            if (await context.Countries.AnyAsync())
                return;

            var countries = new List<Country> {
                new() { ArName = "مصر", EnName = "Egypt" },
            };

            await context.Countries.AddRangeAsync(countries, ct);

            await context.SaveChangesAsync(ct);
        }
    }
}
