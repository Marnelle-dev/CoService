using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class PartenaireConfiguration : IEntityTypeConfiguration<Partenaire>
{
    public void Configure(EntityTypeBuilder<Partenaire> builder)
    {
        builder.ToTable("Partenaires");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(p => p.CodePartenaire)
            .HasColumnName("CodePartenaire")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(p => p.CodePartenaire)
            .IsUnique()
            .HasDatabaseName("IX_Partenaires_CodePartenaire");

        builder.Property(p => p.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Adresse)
            .HasColumnName("Adresse")
            .HasMaxLength(500);

        builder.Property(p => p.Telephone)
            .HasColumnName("Telephone")
            .HasMaxLength(50);

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasMaxLength(255);

        builder.Property(p => p.TypePartenaireId)
            .HasColumnName("TypePartenaireId");

        builder.Property(p => p.DepartementId)
            .HasColumnName("DepartementId");

        builder.Property(p => p.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.DerniereSynchronisation)
            .HasColumnName("DerniereSynchronisation")
            .HasColumnType("datetime2(7)");

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
        builder.HasOne(p => p.TypePartenaire)
            .WithMany(tp => tp.Partenaires)
            .HasForeignKey(p => p.TypePartenaireId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.Departement)
            .WithMany(d => d.Partenaires)
            .HasForeignKey(p => p.DepartementId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation avec Certificats sera ajoutée quand CertificatOrigine sera modifié
        // builder.HasMany(p => p.Certificats)
        //     .WithOne()
        //     .HasForeignKey(c => c.PartenaireId)
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Exportateurs)
            .WithOne(e => e.Partenaire)
            .HasForeignKey(e => e.PartenaireId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.ZonesProductions)
            .WithOne(zp => zp.Partenaire)
            .HasForeignKey(zp => zp.PartenaireId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
