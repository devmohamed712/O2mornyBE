using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;
using O2morny.Domain.Common.Enums;
using O2morny.Infrastructure.Persistence.Identity;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.Id);

            // 1:1 relationship with IdentityUser
            builder.HasOne<ApplicationUser>()
                .WithOne(x => x.Account)
                .HasForeignKey<Account>(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(x => x.CityId)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.ProfilePicture)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .HasDefaultValue(AccountStatus.Active)
                .IsRequired();

            builder.HasOne(x => x.City)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}