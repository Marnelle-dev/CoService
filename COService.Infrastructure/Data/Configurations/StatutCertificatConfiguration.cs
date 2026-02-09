using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

/// <summary>
/// Configuration EF Core pour l'entité StatutCertificat.
/// </summary>
public class StatutCertificatConfiguration : IEntityTypeConfiguration<StatutCertificat>
{
    public void Configure(EntityTypeBuilder<StatutCertificat> builder)
    {
        builder.ToTable("StatutsCertificats");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(s => s.Code)
            .HasColumnName("Code")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(s => s.Code)
            .IsUnique()
            .HasDatabaseName("IX_StatutsCertificats_Code");

        builder.Property(s => s.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(100)
            .IsRequired();

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
