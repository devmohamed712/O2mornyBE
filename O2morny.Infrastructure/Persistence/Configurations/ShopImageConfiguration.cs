using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class ShopImageConfiguration : IEntityTypeConfiguration<ShopImage>
    {
        public void Configure(EntityTypeBuilder<ShopImage> builder)
        {
            builder.ToTable("ShopImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ShopId)
                .IsRequired();

            builder.Property(x => x.Image)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.IsMain)
                .IsRequired();

            builder.HasIndex(x => x.ShopId)
                .IsUnique()
                .HasFilter("[IsMain] = 1");
        }
    }
}
