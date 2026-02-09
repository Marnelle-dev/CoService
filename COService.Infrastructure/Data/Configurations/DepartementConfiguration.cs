using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class DepartementConfiguration : IEntityTypeConfiguration<Departement>
{
    public void Configure(EntityTypeBuilder<Departement> builder)
    {
        builder.ToTable("Departements");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(d => d.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(d => d.Code)
            .IsUnique()
            .HasDatabaseName("IX_Departements_Code");

        builder.Property(d => d.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(d => d.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(d => d.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(d => d.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(d => d.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(d => d.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(d => d.Partenaires)
            .WithOne(p => p.Departement)
            .HasForeignKey(p => p.DepartementId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(d => d.Exportateurs)
            .WithOne(e => e.Departement)
            .HasForeignKey(e => e.DepartementId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
