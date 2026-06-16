using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace O2morny.Infrastructure.Persistence
{
    public class O2mornyContextFactory : IDesignTimeDbContextFactory<O2mornyContext>
    {
        public O2mornyContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("SqlConnection");
            var optionsBuilder = new DbContextOptionsBuilder<O2mornyContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new O2mornyContext(optionsBuilder.Options);
        }
    }
}
