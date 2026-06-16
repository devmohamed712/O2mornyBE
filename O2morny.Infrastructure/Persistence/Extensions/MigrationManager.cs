using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace O2morny.Infrastructure.Persistence.Extensions
{
    public static class MigrationManager
    {
        public static async Task<WebApplication> ApplyMigrationsAsync(
            this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<O2mornyContext>();

            await context.Database.MigrateAsync();

            return app;
        }
    }
}
