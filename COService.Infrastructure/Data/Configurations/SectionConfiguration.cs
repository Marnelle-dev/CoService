using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(s => s.Code)
            .HasColumnName("Code")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(s => s.Code)
            .IsUnique()
            .HasDatabaseName("IX_Sections_Code");

        builder.Property(s => s.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(s => s.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(s => s.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(s => s.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(s => s.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(s => s.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");
    }
}
