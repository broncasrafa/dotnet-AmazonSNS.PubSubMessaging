using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CloudNotes.Domain.Entities;
using CloudNotes.Infra.Data.Configurations.Common;

namespace CloudNotes.Infra.Data.Configurations;

internal class NoteConfiguration : BaseEntityConfiguration<Note>
{
    protected override void AppendEntityConfiguration(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Note");

        builder.Property(c => c.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("nvarchar(255)");

        builder.Property(c => c.Content)
            .IsRequired()
            .HasColumnName("Content")
        .HasColumnType("nvarchar(max)");

        builder.Property(c => c.UserId)
            .IsRequired(false)
            .HasColumnName("UserId");
    }
}
    /*
CREATE TABLE Note(
    Id TEXT NOT NULL PRIMARY KEY UNIQUE, -- Armazena o GUID como texto
    Title TEXT NOT NULL CHECK(length(Title) <= 50), -- Limita o título a 50 caracteres
    Content TEXT NOT NULL, -- Conteúdo sem limite
    UserId INTEGER, -- Inteiro opcional
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP, -- Valor padrão é a data/hora atual
    UpdatedAt DATETIME -- Campo opcional
);
*/