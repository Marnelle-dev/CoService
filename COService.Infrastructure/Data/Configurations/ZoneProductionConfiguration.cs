using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class ZoneProductionConfiguration : IEntityTypeConfiguration<ZoneProduction>
{
    public void Configure(EntityTypeBuilder<ZoneProduction> builder)
    {
        builder.ToTable("ZonesProductions");

        builder.HasKey(zp => zp.Id);

        builder.Property(zp => zp.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(zp => zp.PartenaireId)
            .HasColumnName("PartenaireId")
            .IsRequired();

        builder.Property(zp => zp.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(zp => zp.Description)
            .HasColumnName("Description")
            .HasMaxLength(500);

        // Champs d'audit
        builder.Property(zp => zp.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(zp => zp.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(zp => zp.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(zp => zp.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(zp => zp.Partenaire)
            .WithMany(p => p.ZonesProductions)
            .HasForeignKey(zp => zp.PartenaireId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation avec Certificats sera ajoutée quand CertificatOrigine sera modifié
        // builder.HasMany(zp => zp.Certificats)
        //     .WithOne()
        //     .HasForeignKey(c => c.ZoneProductionId)
        //     .OnDelete(DeleteBehavior.SetNull);
    }
}
