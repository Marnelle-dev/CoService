using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class AbonnementConfiguration : IEntityTypeConfiguration<Abonnement>
{
    public void Configure(EntityTypeBuilder<Abonnement> builder)
    {
        builder.ToTable("Abonnements");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(a => a.Exportateur)
            .HasColumnName("exportateur")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Partenaire)
            .HasColumnName("partenaire")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.TypeCO)
            .HasColumnName("type_co")
            .HasMaxLength(100);

        builder.Property(a => a.FactureNo)
            .HasColumnName("factureNo")
            .HasMaxLength(255);

        builder.Property(a => a.Formule)
            .HasColumnName("formule")
            .HasMaxLength(200);

        builder.Property(a => a.Numero)
            .HasColumnName("numero")
            .HasMaxLength(255);

        builder.Property(a => a.Statut)
            .HasColumnName("Statut")
            .HasMaxLength(200);

        builder.Property(a => a.CertificateId)
            .HasColumnName("certificate_id")
            .IsRequired(false);

        // Relation optionnelle avec CertificatOrigine (certificat principal)
        // Note: Cette relation est indépendante de la relation 1-N via Certificats
        builder.HasOne(a => a.Certificate)
            .WithOne()
            .HasForeignKey<Abonnement>(a => a.CertificateId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        // Champs d'audit
        builder.Property(a => a.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(a => a.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(a => a.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(a => a.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relation 1-n avec CertificatOrigine
        builder.HasMany(a => a.Certificats)
            .WithOne(c => c.Abonnement)
            .HasForeignKey(c => c.AbonnementId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

