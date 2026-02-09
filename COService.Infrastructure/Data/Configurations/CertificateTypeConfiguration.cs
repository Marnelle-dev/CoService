using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class CertificateTypeConfiguration : IEntityTypeConfiguration<CertificateType>
{
    public void Configure(EntityTypeBuilder<CertificateType> builder)
    {
        builder.ToTable("TypesCertificats");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(ct => ct.Designation)
            .HasColumnName("designation")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(ct => ct.Code)
            .HasColumnName("code")
            .HasMaxLength(50)
            .IsRequired();

        // Champs d'audit
        builder.Property(ct => ct.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(ct => ct.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(ct => ct.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(ct => ct.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relation 1-N avec CertificatOrigine
        builder.HasMany(ct => ct.Certificats)
            .WithOne(c => c.Type)
            .HasForeignKey(c => c.TypeId)
            .OnDelete(DeleteBehavior.SetNull);

        // Index unique sur le code
        builder.HasIndex(ct => ct.Code)
            .IsUnique()
            .HasDatabaseName("IX_TypesCertificats_code");
    }
}

