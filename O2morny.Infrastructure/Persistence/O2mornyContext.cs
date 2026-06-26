using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Domain.Common.Entities;
using O2morny.Domain.Common.Interfaces;
using O2morny.Infrastructure.Persistence.Identity;
using System.Reflection;

namespace O2morny.Infrastructure.Persistence
{
    public class O2mornyContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
    {
        public O2mornyContext(DbContextOptions<O2mornyContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ServiceProviderProfile> ServiceProviderProfiles { get; set; }
        public DbSet<WhatsappOtp> WhatsappOtps { get; set; }
        public DbSet<Message> Messages { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
            => base.SaveChangesAsync(ct);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(x => x.PhoneNumber)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasIndex(x => x.PhoneNumber)
                    .IsUnique();
            });

            builder.Entity<ApplicationRole>()
                .ToTable("Roles");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens");

            ApplySoftDeleteQueryFilter(builder);


            builder.ApplyConfigurationsFromAssembly(typeof(O2mornyContext).Assembly);
        }


        private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(O2mornyContext).GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!;

        private static void ApplySoftDeleteQueryFilter(ModelBuilder modelBuilder)
        {
            var softDeleteEntities = modelBuilder.Model
                .GetEntityTypes()
                .Where(x => typeof(ISoftDelete).IsAssignableFrom(x.ClrType));

            foreach (var entityType in softDeleteEntities)
            {
                var method = SetSoftDeleteFilterMethod
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, new object[] { modelBuilder });
            }
        }

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class, ISoftDelete
        {
            modelBuilder.Entity<TEntity>()
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }
}