using System;
using COService.Domain.Entities;
using COService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class CertificatOrigineConfiguration : IEntityTypeConfiguration<CertificatOrigine>
{
    public void Configure(EntityTypeBuilder<CertificatOrigine> builder)
    {
        builder.ToTable("certificates");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(c => c.CertificateNo)
            .HasColumnName("CertificateNo")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(c => c.CertificateNo)
            .IsUnique()
            .HasDatabaseName("IX_certificates_CertificateNo");

        builder.Property(c => c.Exportateur)
            .HasColumnName("Exportateur")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Partenaire)
            .HasColumnName("Partenaire")
            .HasMaxLength(200);

        builder.Property(c => c.PaysDestination)
            .HasColumnName("PaysDestination")
            .HasMaxLength(200);

        builder.Property(c => c.PortSortie)
            .HasColumnName("PortSortie")
            .HasMaxLength(200);

        builder.Property(c => c.PortCongo)
            .HasColumnName("PortCongo")
            .HasMaxLength(200);

        builder.Property(c => c.TypeId)
            .HasColumnName("TypeId");

        // Relation avec CertificateType (optionnelle)
        builder.HasOne(c => c.Type)
            .WithMany(ct => ct.Certificats)
            .HasForeignKey(c => c.TypeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(c => c.Formule)
            .HasColumnName("Formule")
            .HasMaxLength(200);

        builder.Property(c => c.Mandataire)
            .HasColumnName("Mandataire")
            .HasMaxLength(200);

        builder.Property(c => c.Statut)
            .HasColumnName("Statut")
            .HasMaxLength(30)
            .HasConversion(
                v => ConvertStatutToString(v),
                v => ConvertStringToStatut(v))
            .IsRequired();

        // CHECK constraint pour Statut (selon le dictionnaire de données)
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_certificates_Statut",
            "Statut IN ('Élaboré', 'Soumis', 'Contrôlé', 'Approuvé', 'Validé')"));

        builder.Property(c => c.Observation)
            .HasColumnName("Observation")
            .HasColumnType("nvarchar(max)");

        builder.Property(c => c.ProductsRecipientName)
            .HasColumnName("ProductsRecipientName")
            .HasMaxLength(255);

        builder.Property(c => c.ProductsRecipientAddress1)
            .HasColumnName("ProductsRecipientAddress1")
            .HasMaxLength(255);

        builder.Property(c => c.ProductsRecipientAddress2)
            .HasColumnName("ProductsRecipientAddress2")
            .HasMaxLength(255);

        builder.Property(c => c.Navire)
            .HasColumnName("navire")
            .HasMaxLength(255);

        builder.Property(c => c.DocumentsId)
            .HasColumnName("documents_id");

        builder.Property(c => c.AbonnementId)
            .HasColumnName("abonnement_id");

        // Champs d'audit
        builder.Property(c => c.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)")
            .IsRequired();

        builder.Property(c => c.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(c => c.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(c => c.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasMany(c => c.CertificateLines)
            .WithOne(cl => cl.CertificatOrigine)
            .HasForeignKey(cl => cl.CertificateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.CertificateValidations)
            .WithOne(cv => cv.CertificatOrigine)
            .HasForeignKey(cv => cv.CertificateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Commentaires)
            .WithOne(com => com.CertificatOrigine)
            .HasForeignKey(com => com.CertificateId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation avec Abonnement (optionnelle, un certificat peut ne pas avoir d'abonnement)
        builder.HasOne(c => c.Abonnement)
            .WithMany(a => a.Certificats)
            .HasForeignKey(c => c.AbonnementId)
            .OnDelete(DeleteBehavior.SetNull);
    }

    private static string ConvertStatutToString(StatutCertificat statut)
    {
        return statut switch
        {
            StatutCertificat.Elabore => "Élaboré",
            StatutCertificat.Soumis => "Soumis",
            StatutCertificat.Controle => "Contrôlé",
            StatutCertificat.Approuve => "Approuvé",
            StatutCertificat.Valide => "Validé",
            _ => statut.ToString()
        };
    }

    private static StatutCertificat ConvertStringToStatut(string statut)
    {
        return statut switch
        {
            "Élaboré" => StatutCertificat.Elabore,
            "Soumis" => StatutCertificat.Soumis,
            "Contrôlé" => StatutCertificat.Controle,
            "Approuvé" => StatutCertificat.Approuve,
            "Validé" => StatutCertificat.Valide,
            _ => throw new ArgumentException($"Valeur de statut invalide : {statut}")
        };
    }
}

