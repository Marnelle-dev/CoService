using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class CertificateValidationConfiguration : IEntityTypeConfiguration<CertificateValidation>
{
    public void Configure(EntityTypeBuilder<CertificateValidation> builder)
    {
        builder.ToTable("certificate_validations");

        builder.HasKey(cv => cv.Id);

        builder.Property(cv => cv.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(cv => cv.CertificateId)
            .HasColumnName("certificate_id")
            .IsRequired();

        builder.Property(cv => cv.Etape)
            .HasColumnName("Etape")
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(cv => cv.RoleVisa)
            .HasColumnName("RoleVisa")
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Property(cv => cv.VisaPar)
            .HasColumnName("VisaPar")
            .HasMaxLength(200);

        builder.Property(cv => cv.Commentaire)
            .HasColumnName("Commentaire")
            .HasMaxLength(255);

        // Champs d'audit
        builder.Property(cv => cv.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(cv => cv.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(cv => cv.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(cv => cv.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");
    }
}

