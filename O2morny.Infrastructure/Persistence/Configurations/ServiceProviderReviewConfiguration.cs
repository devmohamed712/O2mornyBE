using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class ServiceProviderReviewConfiguration : IEntityTypeConfiguration<ServiceProviderReview>
    {
        public void Configure(EntityTypeBuilder<ServiceProviderReview> builder)
        {
            builder.ToTable("ServiceProviderReviews");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ServiceProviderProfileId)
                   .IsRequired();

            builder.Property(x => x.ClientAccountId)
                   .IsRequired();

            builder.Property(x => x.Rating)
                   .HasPrecision(3, 1);

            builder.Property(x => x.Review)
                   .IsRequired()
                   .HasMaxLength(4000);

            builder.HasOne(x => x.ServiceProviderProfile)
                   .WithMany(x => x.ReceivedReviews)
                   .HasForeignKey(x => x.ServiceProviderProfileId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ClientAccount)
                   .WithMany(x => x.WrittenReviews)
                   .HasForeignKey(x => x.ClientAccountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
