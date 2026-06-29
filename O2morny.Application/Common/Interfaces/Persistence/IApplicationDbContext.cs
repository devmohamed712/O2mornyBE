using Microsoft.EntityFrameworkCore;
using O2morny.Domain.Common.Entities;

namespace O2morny.Application.Common.Interfaces.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Country> Countries { get; }
        DbSet<City> Cities { get; }
        DbSet<Account> Accounts { get; }
        DbSet<ServiceProviderProfile> ServiceProviderProfiles { get; set; }
        DbSet<ServiceProviderReview> ServiceProviderReviews { get; set; }
        DbSet<WhatsappOtp> WhatsappOtps { get; }
        DbSet<Message> Messages { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}