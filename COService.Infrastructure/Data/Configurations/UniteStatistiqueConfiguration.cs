using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class UniteStatistiqueConfiguration : IEntityTypeConfiguration<UniteStatistique>
{
    public void Configure(EntityTypeBuilder<UniteStatistique> builder)
    {
        builder.ToTable("UniteStatistiques");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(u => u.Code)
            .HasColumnName("Code")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(u => u.Code)
            .IsUnique()
            .HasDatabaseName("IX_UniteStatistiques_Code");

        builder.Property(u => u.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(u => u.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(u => u.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(u => u.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(u => u.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");
    }
}
