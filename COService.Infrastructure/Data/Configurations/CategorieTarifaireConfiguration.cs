using COService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COService.Infrastructure.Data.Configurations;

public class CategorieTarifaireConfiguration : IEntityTypeConfiguration<CategorieTarifaire>
{
    public void Configure(EntityTypeBuilder<CategorieTarifaire> builder)
    {
        builder.ToTable("CategoriesTariffaires");

        builder.HasKey(cat => cat.Id);

        builder.Property(cat => cat.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(cat => cat.Code)
            .HasColumnName("Code")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(cat => cat.Code)
            .IsUnique()
            .HasDatabaseName("IX_CategoriesTariffaires_Code");

        builder.Property(cat => cat.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(cat => cat.DivisionCode)
            .HasColumnName("DivisionCode")
            .HasMaxLength(10);

        builder.Property(cat => cat.DivisionId)
            .HasColumnName("DivisionId");

        builder.Property(cat => cat.Actif)
            .HasColumnName("Actif")
            .IsRequired()
            .HasDefaultValue(true);

        // Champs d'audit
        builder.Property(cat => cat.CreeLe)
            .HasColumnName("CreeLe")
            .HasColumnType("datetime2(7)");

        builder.Property(cat => cat.CreePar)
            .HasColumnName("CreePar")
            .HasColumnType("nvarchar(max)");

        builder.Property(cat => cat.ModifierLe)
            .HasColumnName("ModifierLe")
            .HasColumnType("datetime2(7)");

        builder.Property(cat => cat.ModifiePar)
            .HasColumnName("ModifiePar")
            .HasColumnType("nvarchar(max)");

        // Relations
        builder.HasOne(cat => cat.Division)
            .WithMany(dt => dt.CategoriesTariffaires)
            .HasForeignKey(cat => cat.DivisionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(cat => cat.PositionsTariffaires)
            .WithOne(pt => pt.Categorie)
            .HasForeignKey(pt => pt.CategorieCodeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
