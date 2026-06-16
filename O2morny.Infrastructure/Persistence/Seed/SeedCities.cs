using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Seed
{
    public static class SeedCities
    {
        public static async Task SeedCitiesAsync(this WebApplication app, CancellationToken ct)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<O2mornyContext>();

            if (await context.Cities.AnyAsync())
                return;

            var cities = new List<City> {
                new() { ArName = "القاهرة", EnName = "Cairo", CountryId = 1 },
                new() { ArName = "الجيزة", EnName = "Giza", CountryId = 1 },
                new() { ArName = "الإسكندرية", EnName = "Alexandria", CountryId = 1 },
                new() { ArName = "الدقهلية", EnName = "Dakahlia", CountryId = 1 },
                new() { ArName = "البحر الأحمر", EnName = "Red Sea", CountryId = 1 },
                new() { ArName = "البحيرة", EnName = "Beheira", CountryId = 1 },
                new() { ArName = "الفيوم", EnName = "Fayoum", CountryId = 1 },
                new() { ArName = "الغربية", EnName = "Gharbia", CountryId = 1 },
                new() { ArName = "الإسماعيلية", EnName = "Ismailia", CountryId = 1 },
                new() { ArName = "المنوفية", EnName = "Menofia", CountryId = 1 },
                new() { ArName = "المنيا", EnName = "Minya", CountryId = 1 },
                new() { ArName = "القليوبية", EnName = "Qaliubiya", CountryId = 1 },
                new() { ArName = "الوادي الجديد", EnName = "New Valley", CountryId = 1 },
                new() { ArName = "السويس", EnName = "Suez", CountryId = 1 },
                new() { ArName = "أسوان", EnName = "Aswan", CountryId = 1 },
                new() { ArName = "أسيوط", EnName = "Assiut", CountryId = 1 },
                new() { ArName = "بني سويف", EnName = "Beni Suef", CountryId = 1 },
                new() { ArName = "بورسعيد", EnName = "Port Said", CountryId = 1 },
                new() { ArName = "دمياط", EnName = "Damietta", CountryId = 1 },
                new() { ArName = "الشرقية", EnName = "Sharkia", CountryId = 1 },
                new() { ArName = "جنوب سيناء", EnName = "South Sinai", CountryId = 1 },
                new() { ArName = "كفر الشيخ", EnName = "Kafr El Sheikh", CountryId = 1 },
                new() { ArName = "مطروح", EnName = "Matrouh", CountryId = 1 },
                new() { ArName = "الأقصر", EnName = "Luxor", CountryId = 1 },
                new() { ArName = "قنا", EnName = "Qena", CountryId = 1 },
                new() { ArName = "شمال سيناء", EnName = "North Sinai", CountryId = 1 },
                new() { ArName = "سوهاج", EnName = "Sohag", CountryId = 1 }
            };

            await context.Cities.AddRangeAsync(cities, ct);

            await context.SaveChangesAsync(ct);
        }
    }
}
