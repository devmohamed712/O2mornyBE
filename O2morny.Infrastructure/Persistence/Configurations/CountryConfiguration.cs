using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

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

            builder.HasIndex(x => x.ArName)
                .IsUnique();

            builder.HasIndex(x => x.EnName)
                .IsUnique();

            builder.HasMany(x => x.Cities)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}