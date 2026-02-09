using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class ExportateurConfiguration : IEntityTypeConfiguration<Exportateur>
{
    public void Configure(EntityTypeBuilder<Exportateur> builder)
    {
        builder.ToTable("Exportateurs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(e => e.CodeExportateur)
            .HasColumnName("CodeExportateur")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => e.CodeExportateur)
            .IsUnique()
            .HasDatabaseName("IX_Exportateurs_CodeExportateur");

        builder.Property(e => e.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.RaisonSociale)
            .HasColumnName("RaisonSociale")
            .HasMaxLength(255);

        builder.Property(e => e.NIU)
            .HasColumnName("NIU")
            .HasMaxLength(50);

        builder.Property(e => e.RCCM)
            .HasColumnName("RCCM")
            .HasMaxLength(50);

        builder.Property(e => e.CodeActivite)
            .HasColumnName("CodeActivite")
            .HasMaxLength(50);

        builder.Property(e => e.Adresse)
            .HasColumnName("Adresse")
            .HasMaxLength(500);

        builder.Property(e => e.Telephone)
            .HasColumnName("Telephone")
            .HasMaxLength(50);

        builder.Property(e => e.Email)
            .HasColumnName("Email")
            .HasMaxLength(255);

        builder.Property(e => e.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.PartenaireId)
            .HasColumnName("PartenaireId");

        builder.Property(e => e.DepartementId)
            .HasColumnName("DepartementId");

        builder.Property(e => e.TypeExportateur)
            .HasColumnName("TypeExportateur");

        builder.Property(e => e.DerniereSynchronisation)
            .HasColumnName("DerniereSynchronisation")
            .HasColumnType("datetime2(7)");

        // Champs d'audit
        builder.Property(e => e.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(e => e.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(e => e.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(e => e.Partenaire)
            .WithMany(p => p.Exportateurs)
            .HasForeignKey(e => e.PartenaireId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Departement)
            .WithMany(d => d.Exportateurs)
            .HasForeignKey(e => e.DepartementId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation avec Certificats sera ajoutée quand CertificatOrigine sera modifié
        // builder.HasMany(e => e.Certificats)
        //     .WithOne()
        //     .HasForeignKey(c => c.ExportateurId)
        //     .OnDelete(DeleteBehavior.Restrict);
    }
}
