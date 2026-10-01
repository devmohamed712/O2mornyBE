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
                new() { ArName = "السعودية", EnName = "Saudi Arabia" },
                new() { ArName = "الإمارات", EnName = "UAE" },
                new() { ArName = "الكويت", EnName = "Kuwait" },
                new() { ArName = "قطر", EnName = "Qatar" },
                new() { ArName = "البحرين", EnName = "Bahrain" },
                new() { ArName = "عمان", EnName = "Oman" },
                new() { ArName = "اليمن", EnName = "Yemen" },
                new() { ArName = "الأردن", EnName = "Jordan" },
                new() { ArName = "لبنان", EnName = "Lebanon" },
                new() { ArName = "سوريا", EnName = "Syria" },
                new() { ArName = "العراق", EnName = "Iraq" },
                new() { ArName = "فلسطين", EnName = "Palestine" },
                new() { ArName = "ليبيا", EnName = "Libya" },
                new() { ArName = "تونس", EnName = "Tunisia" },
                new() { ArName = "الجزائر", EnName = "Algeria" },
                new() { ArName = "المغرب", EnName = "Morocco" },
                new() { ArName = "موريتانيا", EnName = "Mauritania" },
                new() { ArName = "السودان", EnName = "Sudan" },
                new() { ArName = "الصومال", EnName = "Somalia" },
                new() { ArName = "جيبوتي", EnName = "Djibouti" },
                new() { ArName = "جزر القمر", EnName = "Comoros" },
            };

            await context.Countries.AddRangeAsync(countries, ct);

            await context.SaveChangesAsync(ct);
        }
    }
}
