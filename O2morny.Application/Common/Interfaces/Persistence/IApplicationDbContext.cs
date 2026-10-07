using Microsoft.EntityFrameworkCore;
using O2morny.Domain.Common.Entities;

namespace O2morny.Application.Common.Interfaces.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Country> Countries { get; }
        DbSet<City> Cities { get; }
        DbSet<Account> Accounts { get; }
        DbSet<WhatsappOtp> WhatsappOtps { get; }
        DbSet<Shop> Shops { get; }
        DbSet<ShopImage> ShopImages { get; }
        DbSet<ShopWorkingHour> ShopWorkingHours { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}