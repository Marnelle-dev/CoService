using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class AeroportConfiguration : IEntityTypeConfiguration<Aeroport>
{
    public void Configure(EntityTypeBuilder<Aeroport> builder)
    {
        builder.ToTable("Aeroports");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(a => a.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(a => a.Code)
            .IsUnique()
            .HasDatabaseName("IX_Aeroports_Code");

        builder.Property(a => a.Nom)
            .HasColumnName("Nom")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.PaysId)
            .HasColumnName("PaysId");

        builder.Property(a => a.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

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

        // Relations
        builder.HasOne(a => a.Pays)
            .WithMany(p => p.Aeroports)
            .HasForeignKey(a => a.PaysId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
