using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class BureauDedouanementConfiguration : IEntityTypeConfiguration<BureauDedouanement>
{
    public void Configure(EntityTypeBuilder<BureauDedouanement> builder)
    {
        builder.ToTable("BureauxDedouanements");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(b => b.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(b => b.Code)
            .IsUnique()
            .HasDatabaseName("IX_BureauxDedouanements_Code");

        builder.Property(b => b.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(b => b.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(b => b.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(b => b.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(b => b.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(b => b.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");
    }
}
