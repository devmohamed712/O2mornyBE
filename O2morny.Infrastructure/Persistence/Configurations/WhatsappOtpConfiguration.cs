using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;
using O2morny.Domain.Common.Enums;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class WhatsappOtpConfiguration : IEntityTypeConfiguration<WhatsappOtp>
    {
        public void Configure(EntityTypeBuilder<WhatsappOtp> builder)
        {
            builder.ToTable("WhatsappOtps");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.IsUsed)
                .HasDefaultValue(false);

            builder.Property(x => x.Status)
                .HasConversion<int>()
                .HasDefaultValue(OtpStatus.Pending);
        }
    }
}
