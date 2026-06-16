using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ArName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.EnName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(x => x.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
