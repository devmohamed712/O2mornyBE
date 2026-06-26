using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class ServiceProviderProfileConfiguration : IEntityTypeConfiguration<ServiceProviderProfile>
    {
        public void Configure(EntityTypeBuilder<ServiceProviderProfile> builder)
        {
            builder.ToTable("ServiceProviderProfiles");

            builder.HasKey(x => x.AccountId);

            builder.Property(x => x.AccountId)
                   .IsRequired();

            builder.Property(x => x.ExperienceYears)
                   .HasPrecision(3, 1);

            builder.Property(x => x.Description)
                   .HasMaxLength(4000);

            builder.HasOne(x => x.Account)
                   .WithOne(x => x.ServiceProviderProfile)
                   .HasForeignKey<ServiceProviderProfile>(x => x.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
