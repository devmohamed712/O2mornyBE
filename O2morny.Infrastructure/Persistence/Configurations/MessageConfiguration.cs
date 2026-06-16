using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using O2morny.Domain.Common.Entities;

namespace O2morny.Infrastructure.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.SenderId)
                .HasMaxLength(450)
                .IsRequired();

            builder.Property(x => x.ReceiverId)
                .HasMaxLength(450)
                .IsRequired();

            builder.Property(x => x.Content)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
