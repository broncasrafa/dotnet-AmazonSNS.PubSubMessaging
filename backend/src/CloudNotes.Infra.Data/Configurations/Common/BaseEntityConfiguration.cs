using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CloudNotes.Domain.Entities.Common;

namespace CloudNotes.Infra.Data.Configurations.Common;

internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id).IsUnique();

        builder.Property(c => c.CreatedAt)
           .IsRequired()
           .HasColumnName("CreatedAt")
           .HasColumnType("datetime")
           .HasDefaultValueSql("(getdate())");

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime");
    }

    protected abstract void AppendEntityConfiguration(EntityTypeBuilder<T> builder);
}
