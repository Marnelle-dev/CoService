using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class PaysConfiguration : IEntityTypeConfiguration<Pays>
{
    public void Configure(EntityTypeBuilder<Pays> builder)
    {
        builder.ToTable("Pays");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(p => p.Code)
            .HasColumnName("Code")
            .HasMaxLength(3)
            .IsRequired();

        builder.HasIndex(p => p.Code)
            .IsUnique()
            .HasDatabaseName("IX_Pays_Code");

        builder.Property(p => p.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(p => p.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(p => p.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(p => p.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(p => p.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(p => p.Ports)
            .WithOne(port => port.Pays)
            .HasForeignKey(port => port.PaysId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.Aeroports)
            .WithOne(a => a.Pays)
            .HasForeignKey(a => a.PaysId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
