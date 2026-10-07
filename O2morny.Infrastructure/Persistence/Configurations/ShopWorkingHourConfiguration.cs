using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class ShopWorkingHourConfiguration : IEntityTypeConfiguration<ShopWorkingHour>
    {
        public void Configure(EntityTypeBuilder<ShopWorkingHour> builder)
        {
            builder.ToTable("ShopWorkingHours");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ShopId)
                .IsRequired();

            builder.Property(x => x.DayOfWeek)
                .IsRequired();

            builder.Property(x => x.OpenAt)
                .IsRequired();

            builder.Property(x => x.CloseAt)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(x => x.Shop)
                .WithMany(x => x.WorkingHours)
                .HasForeignKey(x => x.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ShopId,
                x.DayOfWeek,
                x.OpenAt,
                x.CloseAt
            })
            .IsUnique();
        }
    }
}
