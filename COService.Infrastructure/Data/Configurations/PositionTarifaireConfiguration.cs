using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class PositionTarifaireConfiguration : IEntityTypeConfiguration<PositionTarifaire>
{
    public void Configure(EntityTypeBuilder<PositionTarifaire> builder)
    {
        builder.ToTable("PositionsTariffaires");

        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(pt => pt.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(pt => pt.Code)
            .IsUnique()
            .HasDatabaseName("IX_PositionsTariffaires_Code");

        builder.Property(pt => pt.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(pt => pt.CategorieCodeId)
            .HasColumnName("CategorieCodeId");

        builder.Property(pt => pt.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(pt => pt.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(pt => pt.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(pt => pt.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(pt => pt.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(pt => pt.Categorie)
            .WithMany(cat => cat.PositionsTariffaires)
            .HasForeignKey(pt => pt.CategorieCodeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
