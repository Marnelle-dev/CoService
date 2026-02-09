using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class CertificateLineConfiguration : IEntityTypeConfiguration<CertificateLine>
{
    public void Configure(EntityTypeBuilder<CertificateLine> builder)
    {
        builder.ToTable("LignesCertificats");

        builder.HasKey(cl => cl.Id);

        builder.Property(cl => cl.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(cl => cl.CertificateId)
            .HasColumnName("certificate_id")
            .IsRequired();

        builder.Property(cl => cl.PositionTarifaireId)
            .HasColumnName("PositionTarifaireId");

        builder.Property(cl => cl.HSCode)
            .HasColumnName("HSCode")
            .HasMaxLength(50);

        builder.Property(cl => cl.LineNatureOfProduct)
            .HasColumnName("LineNatureOfProduct")
            .HasMaxLength(255);

        builder.Property(cl => cl.LineQuantity)
            .HasColumnName("LineQuantity")
            .HasMaxLength(50);

        builder.Property(cl => cl.UniteStatistiqueId)
            .HasColumnName("UniteStatistiqueId");

        builder.Property(cl => cl.LineUnits)
            .HasColumnName("LineUnits")
            .HasMaxLength(50);

        builder.Property(cl => cl.DeviseId)
            .HasColumnName("DeviseId");

        builder.Property(cl => cl.IncotermId)
            .HasColumnName("IncotermId");

        builder.Property(cl => cl.LineGrossWeight)
            .HasColumnName("LineGrossWeight")
            .HasMaxLength(50);

        builder.Property(cl => cl.LineNetWeight)
            .HasColumnName("LineNetWeight")
            .HasMaxLength(50);

        builder.Property(cl => cl.LineFOBValue)
            .HasColumnName("LineFOBValue")
            .HasMaxLength(50);

        builder.Property(cl => cl.LineVolume)
            .HasColumnName("LineVolume")
            .HasMaxLength(50);

        // Champs d'audit
        builder.Property(cl => cl.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(cl => cl.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(cl => cl.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(cl => cl.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations avec les référentiels
        builder.HasOne(cl => cl.PositionTarifaire)
            .WithMany()
            .HasForeignKey(cl => cl.PositionTarifaireId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(cl => cl.UniteStatistique)
            .WithMany()
            .HasForeignKey(cl => cl.UniteStatistiqueId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(cl => cl.Devise)
            .WithMany()
            .HasForeignKey(cl => cl.DeviseId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(cl => cl.Incoterm)
            .WithMany()
            .HasForeignKey(cl => cl.IncotermId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

